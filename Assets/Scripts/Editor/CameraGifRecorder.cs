using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization;

public class GifEncoder
{
    private List<byte[]> frames = new List<byte[]>();

    public void AddFrame(byte[] frame)
    {
        frames.Add(frame);
    }

    public void Save(Stream stream)
    {
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
            // Write the GIF header and logical screen descriptor
            WriteHeader(writer);

            // Write the image data
            WriteImageData(writer);

            // Write the GIF trailer
            WriteTrailer(writer);
        }
    }

    private void WriteHeader(BinaryWriter writer)
    {
        // Write the GIF header
        writer.Write("GIF".ToCharArray());
        writer.Write("89a".ToCharArray());
    }

    private void WriteImageData(BinaryWriter writer)
    {
        foreach (var frame in frames)
        {
            // Write the image separator
            writer.Write((byte)0x2C);

            // Write the image data
            WriteImageDescriptor(writer, frame.Length);
            writer.Write(frame);
        }
    }

    private void WriteImageDescriptor(BinaryWriter writer, int imageLength)
    {
        // Write the image descriptor
        writer.Write((short)0); // Left position
        writer.Write((short)0); // Top position
        writer.Write((short)1920);
        writer.Write((short)1080);
        writer.Write((byte)0);

        // Write the color table
        // (Assuming a simple 256-color palette for simplicity)
        for (int i = 0; i < 256; i++)
        {
            writer.Write((byte)i);
            writer.Write((byte)i);
            writer.Write((byte)i);
        }

        // Write the LZW minimum code size
        writer.Write((byte)8);

        // Write the image data
        writer.Write((byte)imageLength);

        foreach (var iframe in frames)
        {
            writer.Write(iframe);
        }

 
    }

    private void WriteTrailer(BinaryWriter writer)
    {
        // Write the GIF trailer
        writer.Write((byte)0x3B);
    }
}

public class CameraGifRecorder : EditorWindow
{
    public bool IsRecording = false;
    public bool IsRecordingLastFrame = false;
    public string OutputName = "GifFileName";
    int Incrementer = 0;
    Camera RecorderCam = null;
    public static void Init()
    {
        // Get existing open window or, if none, create a new one:
        CameraGifRecorder window = (CameraGifRecorder)EditorWindow.GetWindow(typeof(CameraGifRecorder));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Custom Editor Window", EditorStyles.boldLabel);
        OutputName = EditorGUILayout.TextField("Output File Name:", OutputName);

        RecorderCam = (Camera)EditorGUILayout.ObjectField("Camera to record with:", RecorderCam, typeof(Camera), true);
        if (GUILayout.Button("Run Function"))
        {
            frames.Clear();
            Incrementer = 0;
            IsRecording = true;
      
        }

    }
    private void Update()
    {
        if(IsRecording)
        {
            string directory = Application.persistentDataPath + "/" + OutputName;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            //SaveCameraView(RecorderCam);
            //IsRecordingLastFrame = true;
            Incrementer++; 
            Texture2D frameTexture = CaptureFrame();
            frames.Add(frameTexture);
            if (frames.Count >= 300 || Incrementer >= 300)
            {
                IsRecording = false;
                Incrementer = 0;
                GifMaker(frames);
      
                
            }
        }
      
        //if (IsRecording == false && IsRecordingLastFrame)
        //{
        //   CameraGifRecorder.PNGToGif(OutputName);
        //}
    }
 
    public int frameRate = 30;
    public int width = 1920;
    public int height = 1080;

    private List<Texture2D> frames = new List<Texture2D>();
    private Texture2D CaptureFrame()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        RenderTexture.active = renderTexture;
        Camera.main.targetTexture = renderTexture;
        Camera.main.Render();

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();

        Camera.main.targetTexture = null;
        RenderTexture.active = currentRT;

        return texture;
    }

    private void SaveFramesAsGif(List<Texture2D> frames)
    {
        string filePath = Application.persistentDataPath + "/" + OutputName;

        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            // Create a GIF encoder
            GifEncoder gifEncoder = new GifEncoder();

            foreach (var frameTexture in frames)
            {
                // Convert Texture2D to byte array
                byte[] frameBytes = frameTexture.EncodeToPNG();

                // Add the frame to the GIF
                gifEncoder.AddFrame(frameBytes);
            }

            // Save the GIF to the file
            gifEncoder.Save(fs);
        }

        UnityEngine.Debug.Log("GIF creation complete!");
    }

    private Bitmap Texture2DToBitmap(Texture2D texture)
    {
        Bitmap bitmap = new Bitmap(texture.width, texture.height, PixelFormat.Format32bppArgb);

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color32 color = texture.GetPixel(x, y);
                bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(color.a, color.r, color.g, color.b));
            }
        }

        return bitmap;
    }
    public void GifMaker(List<Texture2D> aFrames)
    {
        List<Bitmap> Bitmaps = new List<Bitmap>();
        foreach(var frame in aFrames)
        {
            var bitFrame = Texture2DToBitmap(frame);
            Bitmaps.Add(bitFrame);
        }
        // Gdi+ constants absent from System.Drawing.
        const int PropertyTagFrameDelay = 0x5100;
        const int PropertyTagLoopCount = 0x5101;
        const short PropertyTagTypeLong = 4;
        const short PropertyTagTypeShort = 3;

        const int UintBytes = 4;

        //...
      
        var gifEncoder = GetEncoder(ImageFormat.Gif);
        // Params of the first frame.
        var encoderParams1 = new EncoderParameters(1);
        encoderParams1.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.MultiFrame);
        // Params of other frames.
        var encoderParamsN = new EncoderParameters(1);
        encoderParamsN.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.FrameDimensionTime);
        // Params for the finalizing call.
        var encoderParamsFlush = new EncoderParameters(1);
        encoderParamsFlush.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.Flush);

        // PropertyItem for the frame delay (apparently, no other way to create a fresh instance).
        var frameDelay = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
        frameDelay.Id = PropertyTagFrameDelay;
        frameDelay.Type = PropertyTagTypeLong;
        // Length of the value in bytes.
        frameDelay.Len = Bitmaps.Count * UintBytes;
        // The value is an array of 4-byte entries: one per frame.
        // Every entry is the frame delay in 1/100-s of a second, in little endian.
        frameDelay.Value = new byte[Bitmaps.Count * UintBytes];
        // E.g., here, we're setting the delay of every frame to 1 second.
        var frameDelayBytes = BitConverter.GetBytes((uint)100);
        for (int j = 0; j < Bitmaps.Count; ++j)
            Array.Copy(frameDelayBytes, 0, frameDelay.Value, j * UintBytes, UintBytes);

        // PropertyItem for the number of animation loops.
        var loopPropertyItem = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
        loopPropertyItem.Id = PropertyTagLoopCount;
        loopPropertyItem.Type = PropertyTagTypeShort;
        loopPropertyItem.Len = 1;
        // 0 means to animate forever.
        loopPropertyItem.Value = BitConverter.GetBytes((ushort)0);

        using (var stream = new FileStream(Application.persistentDataPath + "/" + OutputName + "animation.gif", FileMode.Create))
        {
            bool first = true;
            Bitmap firstBitmap = null;
            // Bitmaps is a collection of Bitmap instances that'll become gif frames.
            foreach (var bitmap in Bitmaps)
            {
                if (first)
                {
                    firstBitmap = bitmap;
                    firstBitmap.SetPropertyItem(frameDelay);
                    firstBitmap.SetPropertyItem(loopPropertyItem);
                    firstBitmap.Save(stream, gifEncoder, encoderParams1);
                    first = false;
                }
                else
                {
                    firstBitmap.SaveAdd(bitmap, encoderParamsN);
                }
            }
            firstBitmap.SaveAdd(encoderParamsFlush);
        }
    }
// ...

private ImageCodecInfo GetEncoder(ImageFormat format)
{
    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
    foreach (ImageCodecInfo codec in codecs)
    {
        if (codec.FormatID == format.Guid)
        {
            return codec;
        }
    }
    return null;
}
    //public string outputFileName = "output.mp4";
    //public int frameRate = 30;
    //public int width = 1920;
    //public int height = 1080;

    //private List<Texture2D> frames = new List<Texture2D>();



    //private Texture2D CaptureFrame()
    //{
    //    RenderTexture currentRT = RenderTexture.active;
    //    RenderTexture renderTexture = new RenderTexture(width, height, 24);
    //    RenderTexture.active = renderTexture;
    //    Camera.main.targetTexture = renderTexture;
    //    Camera.main.Render();

    //    Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
    //    texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    //    texture.Apply();

    //    Camera.main.targetTexture = null;
    //    RenderTexture.active = currentRT;

    //    return texture;
    //}

    //private void SaveFramesAsPNG()
    //{
    //    string directory = Application.persistentDataPath + "/" + OutputName;
    //    if (!Directory.Exists(directory))
    //        Directory.CreateDirectory(directory);

    //    for (int i = 0; i < frames.Count; i++)
    //    {
    //        byte[] bytes = frames[i].EncodeToPNG();
    //        File.WriteAllBytes(Path.Combine(directory, $"frame_{i:D5}.png"), bytes);
    //    }
    //}

    //private void ConvertToVideo()
    //{
    //    ProcessStartInfo psi = new ProcessStartInfo
    //    {
    //        FileName = "ffmpeg",
    //        Arguments = $"-framerate {frameRate} -i Frames/frame_%05d.png -c:v libx264 -pix_fmt yuv420p {outputFileName}",
    //        RedirectStandardOutput = true,
    //        RedirectStandardError = true,
    //        UseShellExecute = false,
    //        CreateNoWindow = true
    //    };

    //    Process process = new Process { StartInfo = psi };
    //    process.Start();
    //    process.WaitForExit();

    //    UnityEngine.Debug.Log("Video conversion complete!");
    //}
}
//public static void RecordFromCamera()
//{

//}
//static void PNGToGif(string aOutputName)
//{
//    // Replace "path/to/frames" with the actual path to your image frames
//    string framesDirectory = Application.persistentDataPath + "/" + aOutputName;
//    string outputGifPath = Application.persistentDataPath + "/" + aOutputName + "/" + "output.gif";

//    // Get a list of image files in the specified directory
//    string[] frameFiles = Directory.GetFiles(framesDirectory, "*.png");

//    // Create a GIF file
//    using (FileStream fs = new FileStream(outputGifPath, FileMode.Create))
//    {
//        // Write the GIF header
//        WriteGifHeader(fs);

//        // Write the image data for each frame
//        foreach (string frameFile in frameFiles)
//        {
//            byte[] imageBytes = File.ReadAllBytes(frameFile);
//            WriteImageDescriptor(fs, imageBytes.Length);
//            fs.Write(imageBytes, 0, imageBytes.Length);
//        }

//        // Write the GIF trailer
//        WriteGifTrailer(fs);
//    }
//    Debug.Log($"Animated GIF saved to: {outputGifPath}");
//}

//static void WriteGifHeader(FileStream fs)
//{
//    // GIF header
//    byte[] header = Encoding.ASCII.GetBytes("GIF89a");
//    fs.Write(header, 0, header.Length);

//    // Logical Screen Descriptor
//    ushort width = 640; // Set the width as needed
//    ushort height = 480; // Set the height as needed
//    byte flags = 0xF7; // Packed field (global color table flag, color resolution, and sort flag)
//    byte backgroundColorIndex = 0;
//    byte pixelAspectRatio = 0;

//    fs.Write(BitConverter.GetBytes(width), 0, 2);
//    fs.Write(BitConverter.GetBytes(height), 0, 2);
//    fs.WriteByte(flags);
//    fs.WriteByte(backgroundColorIndex);
//    fs.WriteByte(pixelAspectRatio);
//}

//static void WriteImageDescriptor(FileStream fs, int imageLength)
//{
//    // Image Descriptor
//    fs.WriteByte(0x2C); // Image separator

//    ushort left = 0;
//    ushort top = 0;
//    ushort imageWidth = 640; // Set the image width as needed
//    ushort imageHeight = 480; // Set the image height as needed
//    byte imageFlags = 0; // Packed field (local color table flag, interlace flag, and sort flag)

//    fs.Write(BitConverter.GetBytes(left), 0, 2);
//    fs.Write(BitConverter.GetBytes(top), 0, 2);
//    fs.Write(BitConverter.GetBytes(imageWidth), 0, 2);
//    fs.Write(BitConverter.GetBytes(imageHeight), 0, 2);
//    fs.WriteByte(imageFlags);

//    // Local Color Table (not used in this example)
//}

//static void WriteGifTrailer(FileStream fs)
//{
//    fs.WriteByte(0x3B); // GIF trailer
//}
//}
//void SaveCameraView(Camera cam)
//{
//    RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
//    cam.targetTexture = screenTexture;
//    RenderTexture.active = screenTexture;
//    cam.Render();
//    Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
//    renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
//    RenderTexture.active = null;
//    byte[] byteArray = renderedTexture.EncodeToPNG();
//    if(!System.IO.Directory.Exists(Application.persistentDataPath + "/" + OutputName))
//    {
//        System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/" + OutputName );
//    }
//    var outputPath = Application.persistentDataPath + "/" + OutputName + "/" + OutputName + Incrementer.ToString() + ".png";
//    UnityEngine.Debug.Log(outputPath);
//    System.IO.File.WriteAllBytes(outputPath, byteArray);
//}
//public string outputFileName = "output.gif";
