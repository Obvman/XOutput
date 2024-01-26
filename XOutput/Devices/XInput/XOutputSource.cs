using XOutput.Devices.Mapper;

namespace XOutput.Devices.XInput
{
    /// <summary>
    /// Direct input source.
    /// </summary>
    public class XOutputSource : InputSource
    {
        public XInputTypes XInputType { get; }

        public XOutputSource(string name, XInputTypes type) : base(null, name, type.GetInputSourceType(), 0)
        {
            XInputType = type;
        }

        internal bool Refresh(InputMapper mapper)
        {
            var mappingCollection = mapper.GetMapping(XInputType);
            if (mappingCollection != null)
            {
                double newValue = mappingCollection.GetValue(XInputType);
                return RefreshValue(newValue);
            }
            return false;
        }
    }
}
