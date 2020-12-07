using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32[] product = new Int32[20];
            long sum;
            double average;
            var path = EnterPath();
            var data = GetData(path);
            Math(data, path, out product, out sum, out average);
            Console.WriteLine(product[0]); //if exeption in 10.txt == 0
            Console.WriteLine(sum);
            Console.WriteLine(average);
        }
        static void Math(Int32[,] data, string path, out Int32[] product, out long sum, out double average)
        {
            if (!File.Exists(path + "overflow.txt"))
            {
                File.CreateText(path + "overflow.txt");
            }
            string overflowList = "";
            product = new Int32[20];
            sum = 0;
            average = 0;
            int count = 0;
            for (int i = 0; i < product.Length; i++)
            {
                try
                {
                    product[i] = checked(data[i, 0] * data[i, 1]);
                }
                catch (OverflowException e)
                {
                    Console.WriteLine("file " + (i+10) + ".txt  " + e + " " + e.Data);
                    if (File.Exists(path + "overflow.txt"))
                    {
                        try
                        {
                            using (StreamWriter writer = new StreamWriter(path + "overflow.txt", true))
                            {
                                {

                                    overflowList += "\n" + (i + 10) + ".txt " + e;
                                    writer.Write(overflowList);


                                }
                                writer.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            //Handle exception
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("file " + i + ".txt  " + e + " " + e.Data);
                }

            }
            for (int i = 0; i < product.Length; i++)
            {
                sum += product[i];
                if(product[i] != 0)
                {
                    count++;
                }
            }
           average = sum / count;
        }
        private static string EnterPath()
        {
            Console.WriteLine("Enter path where your files   Path must be like: Disk:...../YourFiles/");
            var path = Convert.ToString(Console.ReadLine());
            return path;
        }
        private static int[,] GetData(string path)
        {

            Int32[,] data = new int[20,2];
            string no_fileList = "";
            string bad_dataList = "";
            int i = 0;
            if (!File.Exists(path + "no_file.txt"))
            {
                File.CreateText(path + "no_file.txt");
            }
            if (!File.Exists(path + "bad_data.txt"))
            {
                File.CreateText(path + "bad_data.txt");
            }

            /* for (i = 10; i <= 29; i++)
               {
                   if (!File.Exists(path + i + ".txt"))
                   {
                       using (StreamWriter sw = File.CreateText(path + i + ".txt"))
                       {
                           Random rnd = new Random();

                           sw.WriteLine(rnd.Next(-1000,1000));
                           sw.WriteLine(rnd.Next(-100,10000));

                       }

                   }
              }
              */

            for (i = 10; i <= 29; i++)
            {
                try
                {
                    var fileName = Convert.ToString(i) + ".txt";
                    var text = File.ReadAllText(path + fileName);
                    string[] numbers = text.Split("\n");
                    data[i - 10, 0] = Convert.ToInt32(numbers[0]);
                    data[i - 10, 1] = Convert.ToInt32(numbers[1]);
                    Console.WriteLine(i + ".txt ");
                    Console.WriteLine(text);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("file " + i + ".txt  " + e + " " + e.Data);
                    if (File.Exists(path + "bad_data.txt"))
                    {
                        try
                        {
                            using (StreamWriter writer = new StreamWriter(path + "bad_data.txt", true))
                            {
                                {
                                    bad_dataList += "\n" + i + ".txt " + e;
                                    writer.Write(bad_dataList);
                                }
                                writer.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            //Handle exception
                            break;
                        }

                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("file " + i + ".txt  " + e + " " + e.Data);
                    if (File.Exists(path + "no_file.txt"))
                    {
                        try
                        {
                            using (StreamWriter writer = new StreamWriter(path + "no_file.txt", true))
                            {
                                {

                                    // StreamReader reader = new StreamReader(path + "no_file.txt"); // exception
                                    no_fileList += "\n" + i + ".txt " + e;
                                    writer.Write(no_fileList);

                                }
                                writer.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            //Handle exception
                            break;
                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("file " + i + ".txt  " + e + " " + e.Data);
                }
            }
          

            return data;

        } 

    }
}
