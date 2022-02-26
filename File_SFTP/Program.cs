using System;
using System.Buffers.Text;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace File_SFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            
            string source = @"C:\Users\User\source\repos\File_SFTP\Picture.png";
            string destination = @"/iTown_test";
            string sftpfilepath = @"/iTown_test/Picture.png";
            string downloadedpath = @"C:\Picture.jpg";

            string host = "129.126.206.117";
            string username = "testuser2";
            string password = "pass@word1";
            int port = 7250;

            if(input == "up")
            
            SendGetFileToServer.UploadSFTPFile(host, username, password, source, destination, port);
            
            else
            
            SendGetFileToServer.DownloadFile(host, username, password, sftpfilepath, downloadedpath, port);


        }
        public static class SendGetFileToServer
        {
     
            public static void UploadSFTPFile(string host, string username, string password, string sourcefile, string destinationpath, int port)
            {
                using (SftpClient client = new SftpClient(host, port, username, password))
                {
                    client.Connect();
                    client.ChangeDirectory(destinationpath);
                    using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
                    {
                        client.BufferSize = 4 * 1024;
                        client.UploadFile(fs, Path.GetFileName(sourcefile));
                        
                    }
                }

                Console.WriteLine("Successfully Uploaded!");
                Console.ReadLine();
            }
            public static void DownloadFile( string host, string username, string password, string sftpfilepath, string downloadedpath, int port)
            {
                using (SftpClient client = new SftpClient(host, port, username, password))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        byte[] imageByte = client.ReadAllBytes(sftpfilepath);
                        string base64ImageRepresentation = Convert.ToBase64String(imageByte);
                        Console.WriteLine(base64ImageRepresentation);

                    }
                    else
                    {
                        throw new SshConnectionException(String.Format("Can not connect to {0}@{1}", username, host));
                    }

                    Console.ReadLine();
                }
                
            }
          

        }
    

    }
}
