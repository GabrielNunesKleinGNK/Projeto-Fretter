using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Fretter.Util.Extentions
{
    public static class FileExtentions
    {
        private const char CR = '\r';
        private const char LF = '\n';
        private const char NULL = (char)0;

        public static IList<string> ReadAsList(this IFormFile file)
        {
            var result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.GetEncoding("iso-8859-1")))
                while (reader.Peek() >= 0)
                    result.Add(reader.ReadLine());
            return result;
        }
        public static string ReadFirstLine(this IFormFile file)
        {
            string result;
            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.GetEncoding("iso-8859-1")))
                result = reader.ReadLine();
            return result;
        }

        public static byte[] ToByteArray(this Stream str)
        {
            using (var memoryStream = new MemoryStream())
            {
                str.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static int GetStreamLines(this Stream stream)
        {
            int bytesRead, lineCount = 0;

            if (stream == null) return lineCount;

            var byteBuffer = new byte[1024 * 1024];
            var detectedEOL = NULL;
            var currentChar = NULL;

            while ((bytesRead = stream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
            {
                for (var i = 0; i < bytesRead; i++)
                {
                    currentChar = (char)byteBuffer[i];

                    if (detectedEOL != NULL)
                    {
                        if (currentChar == detectedEOL) lineCount++;
                    }
                    else if (currentChar == LF || currentChar == CR)
                    {
                        detectedEOL = currentChar;
                        lineCount++;
                    }
                }
            }

            if (currentChar != LF && currentChar != CR && currentChar != NULL) lineCount++;
            return lineCount;
        }
    }
}