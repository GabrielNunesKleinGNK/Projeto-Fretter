using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Fretter.Domain.Helpers
{
    public static class FileHelper
    {
        public static byte[] ConvertFromBase64String(string input)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            var teste1 = regex.Replace(input, string.Empty);
            return Convert.FromBase64String(teste1);
        }

        public static byte[] StreamToBytes(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public static byte[] ConvertToXlsx(this List<object> list, string sheetName = "Sheet-1", bool haveHeader = true, Dictionary<int,string> customColor = null,bool colorAlternateRow=false)
        {
            byte[] bytes = null;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);
                var currentRow = 1;
                
                if (haveHeader)
                {
                    var headers = ObterHeaderOuDados(list, true);
                    var regex = new Regex("^[0-9]+$");
                    int t = 1;

                    if (colorAlternateRow)
                        worksheet.Row(currentRow).Style.Fill.BackgroundColor = XLColor.FromHtml("d2d1d1");

                    foreach (var header in headers)
                    {
                        
                        worksheet.Cell(currentRow, t).Value = header;
                        worksheet.Cell(currentRow, t).Style.Font.SetBold();
                        if(customColor!=null)
                        {
                            customColor.TryGetValue(t, out string color);
                            if (!string.IsNullOrEmpty(color))
                                worksheet.Cell(currentRow, t).Style.Fill.BackgroundColor = XLColor.FromHtml(color);
                        }

                        t++;
                    }
                }

                var dados = ObterHeaderOuDados(list, false);
                foreach (var item in dados)
                {
                    currentRow++;

                    int i = 1;
                    foreach (var property in item)
                    {
                        worksheet.Cell(currentRow, i).Value = property;
                        i++;
                    }
                    if(colorAlternateRow && currentRow % 2 ==1)
                    {
                        worksheet.Row(currentRow).Style.Fill.BackgroundColor = XLColor.FromHtml("d2d1d1");
                    }
                }
                worksheet.Columns().AdjustToContents();
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    bytes = stream.ToArray();
                }
            }

            return bytes;
        }

        private static List<dynamic> ObterHeaderOuDados(List<dynamic> list, bool header = false)
        {
            var listHeader = new List<dynamic>();
            var listDados = new List<dynamic>();

            if (header) {
                foreach (PropertyInfo l in list.FirstOrDefault().GetType().GetProperties())
                    listHeader.Add(l.Name);
                return listHeader;
            }
            else
            {
                foreach (var l in list)
                {
                    var item = new List<dynamic>();
                    foreach (PropertyInfo lp in l.GetType().GetProperties())
                        item.Add(lp.GetValue(l));

                    listDados.Add(item);
                }
                return listDados;
            }
        }

        public static string RetornaContent(this byte[] arquivo, bool comprimido)
        {
            using (var m = new MemoryStream(comprimido
                ? arquivo.Decompress()
                : arquivo))
            using (var str = new StreamReader(m, Encoding.UTF8, true, 1024, true))
                return str.ReadToEnd();
        }

        public static byte[] Decompress(this byte[] data)
        {
            byte[] ret;
            using (var input = new MemoryStream(data))
            using (var output = new MemoryStream())
            {
                using (var dstream = new DeflateStream(input, CompressionMode.Decompress))
                    dstream.CopyTo(output);
                ret = output.ToArray();
            }
            return ret;
        }
    }
}
