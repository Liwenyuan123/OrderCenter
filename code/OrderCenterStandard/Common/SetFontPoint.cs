using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderCenterStandard.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace OrderCenterStandard.Common
{
    public static class SetFontPoint
    {
        private static ArrayList _FontPoint;
        public static ArrayList FontPoint
        {
            get
            {
                if (_FontPoint == null)
                {
                    _FontPoint = new ArrayList();

                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 5; y++)
                        {
                            _FontPoint.Add(new Models.FontPoint() { X = x * 28, Y = y * 20 });
                        }
                    }
                }
                return _FontPoint;
            }
        }

        /// <summary>
        /// 根据文字和图片获取验证码图片
        /// </summary>
        /// <param name="content"></param>
        /// <param name="picFileName"></param>
        /// <returns></returns>
        public static VerCodePic GetVerCodePic(string content, string picFileName, int fontSize = 20)
        {
            //ClassLoger.Info("FileHelper.GetVerCodePic", "开始生成二维码");
            //Bitmap bmp = new Bitmap(picFileName);
            //List<int> hlist = new List<int>();
            VerCodePic codepic = new VerCodePic();
            //int i = CreateRandomCode（ SetFontPoint.FontPoint.Count - 1);
            //codepic.Font1 = SetFontPoint.FontPoint[i] as FontPoint;
            //hlist.Add(i);

            //A: int i2 = CreateRandomCode( SetFontPoint.FontPoint.Count - 1);
            //if (hlist.Contains(i2))
            //    goto A;
            //codepic.Font2 = SetFontPoint.FontPoint[i2] as FontPoint;
            //hlist.Add(i2);

            //B: int i3 = Utils.GetRandom(0, SetFontPoint.FontPoint.Count - 1);
            //if (hlist.Contains(i3))
            //    goto B;
            //hlist.Add(i3);
            //codepic.Font3 = SetFontPoint.FontPoint[i3] as FontPoint;

            //C: int i4 = Utils.GetRandom(0, SetFontPoint.FontPoint.Count - 1);
            //if (hlist.Contains(i4))
            //    goto C;
            //hlist.Add(i4);
            //codepic.Font4 = SetFontPoint.FontPoint[i4] as FontPoint; string fileName = (content + "-" + picFileName + "-" + i + "|" + i2 + "|" + i3 + "|" + i4).MD5() + Path.GetExtension(picFileName);
            //string dir = Path.Combine(SystemSet.ResourcesPath, SystemSet.VerCodePicPath);
            //string filePath = Path.Combine(dir, fileName);
            //if (File.Exists(filePath))
            //{
            //    codepic.PicURL = string.Format("{0}/{1}/{2}", SystemSet.WebResourcesSite, SystemSet.VerCodePicPath, fileName);
            //    return codepic;
            //}
            //if (!Directory.Exists(dir))
            //{
            //    Directory.CreateDirectory(dir);
            //}

            //Graphics g = Graphics.FromImage(bmp);
            //Font font = new Font("微软雅黑", fontSize, GraphicsUnit.Pixel);
            //SolidBrush sbrush = new SolidBrush(Color.Black);
            //SolidBrush sbrush1 = new SolidBrush(Color.Peru);
            //SolidBrush sbrush2 = new SolidBrush(Color.YellowGreen);
            //SolidBrush sbrush3 = new SolidBrush(Color.SkyBlue);
            //List<char> fontlist = content.ToList();
            //// ClassLoger.Info("FileHelper.GetVerCodePic", fontlist.Count.ToString());
            //g.DrawString(fontlist[0].ToString(), font, sbrush, new PointF(codepic.Font1.X, codepic.Font1.Y));
            //g.DrawString(fontlist[1].ToString(), font, sbrush1, new PointF(codepic.Font2.X, codepic.Font2.Y));
            //g.DrawString(fontlist[2].ToString(), font, sbrush2, new PointF(codepic.Font3.X, codepic.Font3.Y));
            //g.DrawString(fontlist[3].ToString(), font, sbrush3, new PointF(codepic.Font4.X, codepic.Font4.Y));

            //bmp.Save(filePath, ImageFormat.Jpeg);
            //codepic.PicURL = string.Format("{0}/{1}/{2}", SystemSet.WebResourcesSite, SystemSet.VerCodePicPath, fileName);
            return codepic;
        }

       




    }
}