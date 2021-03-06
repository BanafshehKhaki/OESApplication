﻿using System;
using System.IO;
using MathNet.Numerics;

namespace OESApplication.iOS
{
    public class handleSpectra
    {
        public handleSpectra()
        {
        }

        public float[] getAvgSpectrasRows(float[,] data, int height, int width, string name)
        {
            float colsummationinEachRow = 0;
            float[] avgdata = new float[height];
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string filename = Path.Combine(path, DateTime.UtcNow.ToLongDateString() + DateTime.UtcNow.ToLongTimeString() +"_" +name + "_RefRM_Values.txt");
            //using (var streamWriter = new StreamWriter(filename, true))
            //{
                //streamWriter.WriteLine("i , val");
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        colsummationinEachRow += data[i, j];
                    }
                    avgdata[i] = colsummationinEachRow / width;
                    //streamWriter.WriteLine(i + " , " + avgdata[i]);
                    colsummationinEachRow = 0;
                }
            //}
            return avgdata;
        }

        public Tuple<int, float> findPeak(float[] data, int end)
        {
            float maxVal = 0;
            int peaklocation = -1;
            for (int i = 0; i < end; i++)
            {
                if (data[i] > maxVal)
                {
                    maxVal = data[i];
                    peaklocation = i;
                }
            }
            return Tuple.Create(peaklocation, maxVal);
        }


        public double[] CreateWavelenghtToPixelLocationsUsingReferenceSpectra(int bluePeakLocation, int redPeakLocation, int RefLengthanyChannel)
        {

            double[] wavelengthArray = new double[RefLengthanyChannel];

            //r = a x + b;

            double[] xdata = { redPeakLocation, bluePeakLocation };
            double[] ydata = { 610.65, 449.1 };

            Tuple<double, double> p = Fit.Line(xdata, ydata);
            double b = p.Item1; //  intercept
            double a = p.Item2; //  slope
            Console.WriteLine("wavelengthslope: " + a + " ,  intercept: " + b);

            for (int i = 0; i < RefLengthanyChannel; i++)
            {
                wavelengthArray[i] = i * a + b;
                //Console.WriteLine("i "+i+" wavearray: " + wavelengthArray[i]);
            }


            return wavelengthArray;
        }


        public void normalizeArray(float MaxRef, ref float[] data)
        {
            //float[] normalizedArray = new float[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i] / MaxRef;
            }

            //return normalizedArray;
        }


        public double calculateIntensity(float[] NormalizedData, double[] wavelengthArray, string channel, int width, float centerWave)
        {
            //if (channel == "green" || channel =="red")
            //{
                int arg_low = 0, arg_high = 0;
                //double interval = wavelengthArray[0] - wavelengthArray[1];
                double wl_min = (centerWave - (width / 2.0));  // 530 nm for nitrate
                double wl_max = (centerWave + (width / 2.0));  // 540 nm for nitrate

                for (int i = wavelengthArray.Length - 1; i >= 0; i--)
                {
                    if (wavelengthArray[i] > wl_min)
                    {
                        arg_high = i;
                        //Console.WriteLine("arg_low loc: " + arg_low + "wavelengthArray[i]: " + wavelengthArray[i]);
                        break;
                    }
                }
                for (int i = wavelengthArray.Length - 1; i >= 0; i--)
                {
                    if (wavelengthArray[i] < wl_max)
                    {
                        arg_low = i;
                    }
                }
                //Console.WriteLine(" arg_low: " + arg_low + " arg_high: " + arg_high + " wavelengthArray.Length: " + wavelengthArray.Length + " NormalizedDataL: " + NormalizedData.Length);
                //Console.WriteLine(" wl_min: " + wl_min + " wl_max: " + wl_max);

                // error here : investigate

                //double sam_low = ((wl_min - wavelengthArray[arg_low + 1]) * (NormalizedData[arg_low] - NormalizedData[arg_low + 1]) / (wavelengthArray[arg_low] - wavelengthArray[arg_low + 1])) + NormalizedData[arg_low + 1];

                //double sam_high = (((wl_max - wavelengthArray[arg_high]) * (NormalizedData[arg_high - 1] - NormalizedData[arg_high])) / (wavelengthArray[arg_high - 1] - wavelengthArray[arg_high])) + NormalizedData[arg_high];

                double intensity = 0; // sam_low * (wavelengthArray[arg_low] - wl_min) + sam_high * (wl_max - wavelengthArray[arg_high]);


                for (int i = arg_low; i <= arg_high; i++)
                {
                    intensity += NormalizedData[i]; // * (interval);
                    //if (Math.Abs(centerWave - 535) == 0.0) { Console.WriteLine(i + ": " + NormalizedData[i]); }

                }
                //if (Math.Abs(centerWave - 535) == 0.0) { Console.WriteLine("(  arg_high - arg_low + 1):  " + (arg_high - arg_low + 1)); }
                    
                return (intensity / (arg_high - arg_low + 1));
            //}

            //return 0;
        }

        // For now returns intensity 
        public double measureAbsorbance(double sampleIntensity)
        {
            var lgI = Math.Log10(sampleIntensity);
            return (lgI * -1.0);
        }
        // For now returns Absorbance 
        public double measureConcentration(double absorbance, double intercept = -0.14917, double slope = -7.8279)
        {
            return (absorbance * slope + intercept);
        }


    }
}
