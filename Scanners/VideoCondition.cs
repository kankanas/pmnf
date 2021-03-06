using System.IO;
using VidsNet.DataModels;
using VidsNet.Enums;
using VidsNet.Interfaces;

namespace VidsNet.Scanners
{
    public class VideoCondition : IScannerCondition {
        public ScannerConditionResult CheckType(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            var video = new Video();
            if(video.IsVideo(extension))
            {
                return new ScannerConditionResult() { CorrectType = true, Type = Item.Video, WriteVirtualItem = true };
            }
            return new ScannerConditionResult() { CorrectType = false, Type = Item.None, WriteVirtualItem = false };
        }
    }
}