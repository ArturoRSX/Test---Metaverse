# Test---Metaverse
This a Metaverse example for Sumeru Inc.

Created with
Unity – 2021.3.3f1

Full Architecture PDF are inside the Assets folder

Assets
Multiplayer SDK

• Photon Fusion: High performance, low bandwidth, a lot of built-in features.

Multiplayer Chat

• Photon Chat: solution to build a scalable chat.

Multiplayer Voice Chat

• Photon Voice: cross platform, allows to attach an audio source to a 3D object in Unity 
so you can freely place the audio streams, e.g., in a virtual world or an FPS


Notes:
Since Unity doesn’t support Microphone in WebGL. We cannot use Photon Voice to maintain the 
same architecture in WebGL, we need to use a 3rd party plugin.

WebGL Multiplayer Kit – Audio Only

https://assetstore.unity.com/packages/tools/network/webgl-multiplayer-kit-145882

Live Kit SDK – Video & Audio in WebGL

https://github.com/livekit/client-sdk-unity-web

Environment Assets

Used 3D Free Modular KIT – Used for the 3D Environment

Particle Ribborn – Add nice particle effects

Other Assets

Parrel Sync – Allows to test multiplayer gameplay without building the project
