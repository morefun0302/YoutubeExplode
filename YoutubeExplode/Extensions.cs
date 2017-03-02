﻿using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode.Internal;
using YoutubeExplode.Models;

namespace YoutubeExplode
{
    /// <summary>
    /// Extensions for YoutubeExplode
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Downloads video to file
        /// </summary>
        public static void Download(this YoutubeClient client, VideoStreamInfo streamInfo, string filePath)
        {
            if (filePath.IsBlank())
                throw new ArgumentNullException(nameof(filePath));

            // Get stream
            var stream = client.DownloadVideo(streamInfo);

            // Output to fiile
            Directory.CreateDirectory(filePath);
            using (stream)
            using (var fileStream = File.Open(filePath, FileMode.CreateNew, FileAccess.Write))
                stream.CopyTo(fileStream);
        }

        /// <summary>
        /// Downloads video to file
        /// </summary>
        public static async Task DownloadAsync(this YoutubeClient client, VideoStreamInfo streamInfo, string filePath)
        {
            if (filePath.IsBlank())
                throw new ArgumentNullException(nameof(filePath));

            // Get stream
            var stream = await client.DownloadVideoAsync(streamInfo);

            // Output to fiile
            Directory.CreateDirectory(filePath);
            using (stream)
            using (var fileStream = File.Open(filePath, FileMode.CreateNew, FileAccess.Write))
                await stream.CopyToAsync(fileStream);
        }
    }
}