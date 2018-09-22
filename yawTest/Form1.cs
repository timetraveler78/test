using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yawTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        int RDeg = 360;
        int WDeg = 180;
        double AWA = 360;
        private void button1_Click(object sender, EventArgs e)
        {
            double RiderRotation = RDeg;
            double RiderDir = RDeg;
            double AWPointer = AWA;
            double WPointer = WDeg;
            double WSpeedM1, WSpeedM2, WSpeedM3;
            double WSpeedM,WSpeed, Wspeeda, WSpeedb;
            string WSPDText;

            double RSpeed, RSpeeda, RSpeedb;
            double RSpeedM1, RSpeedM2, RSpeedM3, RSpeedM;
            string RSPDText;

            double WAngle;
            Wspeeda = 20;
            WSpeedb = Math.Round(Wspeeda);
            WSpeed = WSpeedb / 5;
            //WSpeedM1 = WSpeed * 1.61;
            //WSpeedM2 = WSpeedM1 * 100;
            //WSpeedM3 = Math.Round(WSpeedM2);
            //WSpeedM = WSpeedM3 / 100;
            WSPDText = WSpeed.ToString();

            RSpeeda = 10;//  (_loc1_.RS._y - _loc1_.RS.inity) * -1;
            RSpeedb = Math.Round(RSpeeda);
            RSpeed = RSpeedb / 5;
            //RSpeedM1 = RSpeed * 1.61;
            //RSpeedM2 = RSpeedM1 * 100;
            //RSpeedM3 = Math.Round(RSpeedM2);
            //RSpeedM = RSpeedM3 / 100;
            RSPDText = RSpeed.ToString();


            if (RDeg > WDeg)
            {
                WAngle = 360 - RDeg + WDeg;
            }
            else
            {
                WAngle = WDeg - RDeg;
            }
            double WARadians, AWSt, AWSn, AWS = 0, WMag, AWAngle2r_1, AWAngle2r, awsttext, awsntext, diffangtext;
            double W2RAngleC, W2RAngleB=0;
                WARadians = WAngle * Math.PI / 180;
            AWSt = -RSpeed + WSpeed * Math.Cos(WARadians);
            AWSn = WSpeed * Math.Sin(WARadians);
            WMag = Math.Round(10 * Math.Sqrt(AWSt * AWSt + AWSn * AWSn)) / 10;
            AWAngle2r_1 = -57.29577951308232 * Math.Atan2(AWSt, AWSn) + 450;
            AWAngle2r = Math.Round(10 * (AWAngle2r_1 - Math.Floor(AWAngle2r_1 / 360) * 360)) / 10;
            awsttext = Math.Round(AWSt * 10) / 10;
            awsntext = Math.Round(AWSn * 10) / 10;
            diffangtext = Math.Round(10 * AWAngle2r) / 10;
            if (AWS != 0)
            {
                W2RAngleC = W2RAngleB / AWS;
            }
            else
            {
                W2RAngleC = 0;
            }

            double W2RAngleD, W2RAngle, AWA1, AWA2, AWA3;
            double AWA2Rider;
            W2RAngleD = Math.Asin(W2RAngleC);
            W2RAngle = W2RAngleD / Math.PI * 180;
            AWA1 = W2RAngle + RDeg;
            AWA2 = AWA1 * 100;
            AWA3 = Math.Round(AWA2);
            AWA = AWA3 / 100;
            if (AWA > 359)
            {
                AWA = AWA - 360;
            }
            if (AWA < 0)
            {
                AWA = 360 + AWA;
            }
            AWA2Rider = AWA - RDeg;
            if (AWA2Rider < 0)
            {
                AWA2Rider = AWA2Rider * -1;
            }
            if (AWA2Rider > 180)
            {
                AWA2Rider = AWA2Rider - 360;
                AWA2Rider = AWA2Rider * -1;
            }
            double AWSpeed, AWAText, AWSpeedM1, AWSpeedM2, AWSpeedM3, AWSpeedM;
            string AWSPDText;
            AWAText = AWAngle2r - 180 - 360 * Math.Floor(AWAngle2r / 360);
            AWSpeed = Math.Round(WMag * 5) / 5;
            //AWSpeedM1 = AWSpeed * 1.61;
            //AWSpeedM2 = AWSpeedM1 * 100;
            //AWSpeedM3 = Math.Round(AWSpeedM2);
            //AWSpeedM = AWSpeedM3 / 100;
            AWSPDText = AWSpeed.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            YawAngle d = new YawAngle();
            d.setApparentWindSpeed();
        }
    }
    class YawAngle
    {
        private double apparentWindAngle;
        private double apparentWindSpeed;

        public void setApparentWindSpeed()
        {
            double WindSpeed, RiderSpeed, TrueAngle;
            WindSpeed = 20;
            RiderSpeed = 20;
            TrueAngle = 90;
            this.apparentWindSpeed = getApparentWindSpeed(WindSpeed, RiderSpeed, ConvertToRadians(TrueAngle));
            this.apparentWindAngle = getApparentWindAngle(TrueAngle, WindSpeed, RiderSpeed);
        }
        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private double getApparentWindSpeed(double WindSpeed, double RiderSpeed, double lambda)
        {
            return Math.Sqrt(Math.Pow(WindSpeed, 2) + Math.Pow(RiderSpeed, 2) + 2 * WindSpeed * RiderSpeed * Math.Cos(lambda));
        }

        private double getApparentWindAngle(double TrueAngle, double WindSpeed, double RiderSpeed)
        {

            var angle = 0;
            var sign = 1; // -1 for left, +1 for right
            if (TrueAngle < 0)
            {
                sign = -1;
                TrueAngle *= -1;
            }
            angle = deg(Math.Atan(WindSpeed * Math.Sin(ConvertToRadians(TrueAngle)) / (RiderSpeed + WindSpeed * Math.Cos(ConvertToRadians(TrueAngle)))));

            if (angle < 0)
            {
                angle = angle + 180;
            }
            return angle * sign;
        }

        private int deg(double x)
        {
            return Convert.ToInt32((x / Math.PI) * 180);
        }
    }
}
