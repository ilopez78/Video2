using System;
using System.Threading.Tasks;

using Plugin.Media;
using Plugin.Media.Abstractions;

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Video2.Services
{
    public class CameraService
    {
        PermissionStatus cameraOK;
        PermissionStatus storageOK;

        public async Task Init()
        {
            await CrossMedia.Current.Initialize();

            cameraOK = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            storageOK = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraOK != PermissionStatus.Granted || storageOK != PermissionStatus.Granted)
            {
                var status = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });

                cameraOK = status[Permission.Camera];
                storageOK = status[Permission.Storage];
            }
        }

       



        public async Task<MediaFile> TakeVideo()
        {
            if (cameraOK == PermissionStatus.Granted
                && storageOK == PermissionStatus.Granted
                && CrossMedia.Current.IsTakeVideoSupported)
            {
                var options = new StoreVideoOptions { SaveToAlbum = true };
                var file = await CrossMedia.Current.TakeVideoAsync(options);
                return file;
            }

            return null;
        }

        public async Task<MediaFile> ChooseVideo()
        {
            if (CrossMedia.Current.IsPickVideoSupported)
            {
                var file = await CrossMedia.Current.PickVideoAsync();
                return file;
            }

            return null;
        }
    }
}
