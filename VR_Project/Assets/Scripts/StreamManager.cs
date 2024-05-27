using System.Collections;
using System.Collections.Generic;
using Unity.WebRTC;
using UnityEngine;
using UnityEngine.UI;

public class StreamManager : MonoBehaviour
{
    public RawImage img;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WebRTC.Update());
        
        var receiveStream = new MediaStream();
        receiveStream.OnAddTrack = e =>
        {
            if (e.Track is VideoStreamTrack track)
            {
                // You can access received texture using `track.Texture` property.
                img.texture = track.Texture;
                Debug.Log(track);
            }
        };

        var peerConnection = new RTCPeerConnection();
        peerConnection.OnTrack = (RTCTrackEvent e) =>
        {
            if (e.Track.Kind == TrackKind.Video)
            {
                // Add track to MediaStream for receiver.
                // This process triggers `OnAddTrack` event of `MediaStream`.
                receiveStream.AddTrack(e.Track);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
