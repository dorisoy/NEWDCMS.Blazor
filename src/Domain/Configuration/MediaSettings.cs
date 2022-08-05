using DCMS.Domain;

namespace DCMS.Domain
{

    public class MediaSettings : ISettings
    {
   
        public int AvatarPictureSize { get; set; }

        public int ProductThumbPictureSize { get; set; }

        public int ProductDetailsPictureSize { get; set; }


        public int ProductThumbPictureSizeOnProductDetailsPage { get; set; }


        public int AssociatedProductPictureSize { get; set; }

        public int CategoryThumbPictureSize { get; set; }


        public int ManufacturerThumbPictureSize { get; set; }


        public int VendorThumbPictureSize { get; set; }

        public int CartThumbPictureSize { get; set; }


        public int MiniCartThumbPictureSize { get; set; }

        public int AutoCompleteSearchThumbPictureSize { get; set; }


        public int ImageSquarePictureSize { get; set; }


        public bool DefaultPictureZoomEnabled { get; set; }


        public int MaximumImageSize { get; set; }

        public int DefaultImageQuality { get; set; }


        public bool MultipleThumbDirectories { get; set; }

        public bool ImportProductImagesUsingHash { get; set; }

        public string AzureCacheControlHeader { get; set; }
        public bool UseAbsoluteImagePath { get; set; }
    }
}