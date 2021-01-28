using System;

namespace ImageConverterLib.ImageProcessing.Models
{
    public class BatchItemModel: IComparable<BatchItemModel>
    {
        public BatchItemModel()
        {

        }

        public BatchItemModel(ImageProcessModel inputFile, ImageProcessModel outputFile)
        {
            InputFile = inputFile;
            OutputFile = outputFile;
        }

        public ImageProcessModel InputFile { get; set; }

        public ImageProcessModel OutputFile { get; set; }

        public bool IsCompleted { get; set; }

        public int SortOrder { get; set; }

        public int GetHashCode(BatchItemModel obj)
        {
            return obj.SortOrder;
        }

        public int CompareTo(BatchItemModel other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            int isCompletedComparison = IsCompleted.CompareTo(other.IsCompleted);
            if (isCompletedComparison != 0) return isCompletedComparison;
            return SortOrder.CompareTo(other.SortOrder);
        }
    }
}