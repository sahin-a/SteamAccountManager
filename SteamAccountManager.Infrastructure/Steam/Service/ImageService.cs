﻿using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Exceptions;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using System;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Service
{
    internal class ImageService : IImageService
    {
        private readonly ImageClient _imageClient;
        private readonly ILogger _logger;

        public ImageService(ImageClient imageClient, ILogger logger)
        {
            _imageClient = imageClient;
            _logger = logger;
        }

        public async Task<byte[]> GetImageAsync(string url)
        {
            try
            {
                return await _imageClient.DownloadImage(url);
            }
            catch (ImageDownloadFailedException e)
            {
                _logger.LogException("Failed to download the image", e);
            }

            return Array.Empty<byte>();
        }
    }
}
