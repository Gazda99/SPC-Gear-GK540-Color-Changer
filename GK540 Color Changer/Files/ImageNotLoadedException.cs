#nullable enable
using System;

namespace GK540_Color_Changer.Files {
[Serializable]
public class ImageNotLoadedException : Exception {
    public ImageNotLoadedException() { }
    public ImageNotLoadedException(string? message) : base(message) { }
}
}