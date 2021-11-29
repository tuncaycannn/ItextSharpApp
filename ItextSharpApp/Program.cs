using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ItextSharpApp
{
    class Program
    {
        public class PdfPage
        {
            public string Content { get; set; }
            public List<PdfRow> Rows { get; set; }
        }
        public class PdfRow
        {
            public string Content { get; set; }
            public List<string> Words { get; set; }
        }
        static void Main(string[] args)
        {
            var pdfPages = new List<PdfPage>();

            var filePath = @"C:\\Users\AG801602\Documents\Alınan Dosyalarım\KKB Çek Örneği.pdf";
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            PdfReader reader = new PdfReader(bytes);

            int pageNumber = reader.NumberOfPages;
            for (int i = 1; i <= pageNumber; i++)
            {
                pdfPages.Add(new PdfPage()
                {
                    Content = PdfTextExtractor.GetTextFromPage(reader, i)
                });
            }

            pdfPages.ForEach(x => x.Rows = x.Content.Split('\n').Select(y =>
                new PdfRow()
                {
                    Content = y,
                    Words = y.Split(' ').ToList()
                }
            ).ToList());

            for (int i = 0; i <= pageNumber; i++)
            {
                for (int j = 0; j < pdfPages[i].Rows.Count; j++)
                {
                    if (pdfPages[i].Rows[j].Content == "ÇEK SKORU")
                    {
                        int t = j + 1;
                        var skor = pdfPages[i].Rows[t].Words[0];
                        break;
                    }
                }
                break;
            }

            for (int i = 0; i <= pageNumber; i++)
            {
                if (pdfPages[i].Content.Contains("Rapor İlgilisinin Hamili veya Son Cirantacısı Olduğu Çekler Son 12 Ay İçin"))
                {
                    for (int j = 0; j < pdfPages[i].Rows.Count; j++)
                    {
                        if (pdfPages[i].Rows[j].Content == "Rapor İlgilisinin Hamili veya Son Cirantacısı Olduğu Çekler Son 12 Ay İçin")
                        {
                            for (int k = j + 1; k < pdfPages[i].Rows.Count; k++)
                            {
                                if (pdfPages[i].Rows[k].Content.Contains("Arkası Yazılan ve Halen Ödenmemiş Çek Adet"))
                                {
                                    var adet = pdfPages[i].Rows[k].Words[7];
                                }

                                if (pdfPages[i].Rows[k].Content.Contains("Arkası Yazılan ve Halen Ödenmemiş Çek Tutar"))
                                {
                                    var tutar = pdfPages[i].Rows[k].Words[7];
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }

        }
    }
}
