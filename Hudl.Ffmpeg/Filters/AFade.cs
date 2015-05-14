using System;
using System.Text;
using Hudl.FFmpeg.BaseTypes;
using Hudl.FFmpeg.Common;
using Hudl.FFmpeg.Filters.Attributes;
using Hudl.FFmpeg.Filters.Attributes.BaseTypes;
using Hudl.FFmpeg.Filters.BaseTypes;
using Hudl.FFmpeg.Filters.Formatters;
using Hudl.FFmpeg.Resources.BaseTypes;

namespace Hudl.FFmpeg.Filters
{
    /// <summary>
    /// Audio filter that applies a fade in or out effect to the audio stream
    /// </summary>
    [ForStream(Type=typeof(AudioStream))]
    [Filter(Name="afade", MaxInputs=1)]
    public class AFade : BaseFilter
    {
        public AFade()
            : base()
        {
        }

        public AFade(double? startUnit, double? lengthInUnits, AudioUnitType unitType)
            : this()
        {
            if (unitType == AudioUnitType.Sample)
            {
                StartSample = startUnit;
                NumberOfSamples = lengthInUnits;
            }
            else
            {
                StartTime = startUnit;
                Duration = lengthInUnits; 
            }
        }

        public AFade(double? startUnit, double? lengthInUnits, AudioUnitType unitType, FadeTransitionType transitionType)
            : this(startUnit, lengthInUnits, unitType)
        {
            TransitionType = transitionType;
        }

        public AFade(double? startUnit, double? lengthInUnits, AudioUnitType unitType, FadeTransitionType transitionType, FadeCurveType curveType)
            : this(startUnit, lengthInUnits, unitType, transitionType)
        {
            CurveType = curveType; 
        }

        [FilterParameter("t")]
        [FilterFormat(typeof(EnumFormatter))]
        [FilterValidate(LogicalOperators.NEq, FadeTransitionType.In)]
        public FadeTransitionType? TransitionType { get; set; }

        [FilterParameter("curve")]
        [FilterFormat(typeof(EnumFormatter))]
        [FilterValidate(LogicalOperators.NEq, FadeCurveType.Tri)]
        public FadeCurveType? CurveType { get; set; }

        [FilterParameter("ss")]
        [FilterValidate(LogicalOperators.Gte, 0D)]
        public double? StartSample { get; set; }
        
        [FilterParameter("ns")]
        [FilterValidate(LogicalOperators.Gte, 0D)]
        public double? NumberOfSamples { get; set; }

        [FilterParameter("st")]
        [FilterValidate(LogicalOperators.Gte, 0D)]
        public double? StartTime { get; set; }

        [FilterParameter("d")]
        [FilterValidate(LogicalOperators.Gte, 0D)]
        public double? Duration { get; set; }
    }
}
