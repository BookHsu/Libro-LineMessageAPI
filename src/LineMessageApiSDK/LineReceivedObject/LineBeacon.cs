namespace LineMessageApiSDK.LineReceivedObject
{
    /// <summary></summary>
    public class LineBeacon
    {
        /// <summary></summary>
        public string dm { get; set; }

        /// <summary></summary>
        public string hwid { get; set; }

        /// <summary></summary> 
        public BeaconType type { get; set; }
    }
}