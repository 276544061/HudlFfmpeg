﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hudl.FFmpeg.BaseTypes;
using Hudl.FFmpeg.Command;
using Hudl.FFmpeg.Filters.BaseTypes;
using Hudl.FFmpeg.Metadata;
using Hudl.FFmpeg.Metadata.BaseTypes;
using Hudl.FFmpeg.Resources.BaseTypes;

namespace Hudl.FFmpeg.Filters
{
    /// <summary>
    /// Concat Filter concatenates multiple resource streams into a collection of output streams
    /// </summary>
    [ForStream(Type=typeof(AudioStream))]
    [ForStream(Type=typeof(VideoStream))]
    public class Concat : BaseFilter, IFilterValidator, IMetadataManipulation
    {
        private const int FilterMaxInputs = 4;
        private const string FilterType = "concat";
        private const int DefaultVideoOut = 1;
        private const int DefaultAudioOut = 0;

        public Concat() 
            : base(FilterType, FilterMaxInputs)
        {
            NumberOfVideoOut = DefaultVideoOut;
            NumberOfAudioOut = DefaultAudioOut;
        }
        public Concat(int? numberOfAudioOut, int? numberOfVideoOut)
            : base(FilterType, FilterMaxInputs)
        {
            NumberOfAudioOut = numberOfAudioOut;
            NumberOfVideoOut = numberOfVideoOut;
        }

        public int? NumberOfVideoOut { get; set; }
        
        public int? NumberOfAudioOut { get; set; }

        public bool UnsafeMode { get; set; }

        public override void Validate()
        {
            var numberOfResources = InputCount;
            if (NumberOfVideoOut.HasValue && NumberOfVideoOut > numberOfResources)
            {
                throw new InvalidOperationException("Number of Videos out cannot be greater than Resources in.");
            }
            if (NumberOfAudioOut.HasValue && NumberOfAudioOut > numberOfResources)
            {
                throw new InvalidOperationException("Number of Audios out cannot be greater than Resources in.");
            }
        }

        public override string ToString()
        {
            var numberOfResources = InputCount;

            var filterParameters = new StringBuilder(100);

            if (numberOfResources > 0)
            {
                FilterUtility.ConcatenateParameter(filterParameters, "n", numberOfResources);
            }
            if (NumberOfVideoOut.HasValue) 
            {
                FilterUtility.ConcatenateParameter(filterParameters, "v", NumberOfVideoOut.GetValueOrDefault());
            }
            if (NumberOfAudioOut != DefaultAudioOut)
            {
                FilterUtility.ConcatenateParameter(filterParameters, "a", NumberOfAudioOut.GetValueOrDefault());
            }
            if (UnsafeMode)
            {
                FilterUtility.ConcatenateParameter(filterParameters, "unsafe");
            }

            return FilterUtility.JoinTypeAndParameters(this, filterParameters);
        }

        #region IFilterValidator
        public bool Validate(FFmpegCommand command, Filterchain filterchain, List<StreamIdentifier> streamIds)
        {
            //concat filters should be used independently of other filters
            return filterchain.Filters.Count == 1;
        }
        #endregion

        public MetadataInfoTreeContainer EditInfo(MetadataInfoTreeContainer infoToUpdate, List<MetadataInfoTreeContainer> suppliedInfo)
        {
            if (NumberOfAudioOut > 0)
            {
                infoToUpdate.AudioStream.Duration = TimeSpan.FromSeconds(suppliedInfo.Sum(i => i.AudioStream.Duration.TotalSeconds));
            }

            if (NumberOfVideoOut > 0)
            {
                infoToUpdate.VideoStream.Duration = TimeSpan.FromSeconds(suppliedInfo.Sum(i => i.VideoStream.Duration.TotalSeconds));
            }

            return infoToUpdate; 
        }
    }
}
