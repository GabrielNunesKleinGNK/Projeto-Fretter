using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Entrada
{
    public partial class EntradaTrackingLoggi
    {
        public List<PackageLoggi> Packages { get; set; }
    }

    public partial class PackageLoggi
    {
        public string LoggiKey { get; set; }
        public string TrackingCode { get; set; }
        public StatusLoggi Status { get; set; }
        public LocationLoggi Location { get; set; }
        public DateTime PromisedDate { get; set; }
        public DateTime RequestTime { get; set; }
        public List<TrackingHistoryLoggi> TrackingHistory { get; set; }
    }

    public partial class LocationLoggi
    {
        public string City { get; set; }
        public string State { get; set; }
    }

    public partial class StatusLoggi
    {
        public string Code { get; set; }
        public string HighLevelStatus { get; set; }
        public string Description { get; set; }
        public ActionRequiredLoggi ActionRequired { get; set; }
        public DateTime UpdatedTime { get; set; }
    }

    public partial class ActionRequiredLoggi
    {
        public string ReasonDescription { get; set; }
        public string ReasonLabel { get; set; }
    }

    public partial class TrackingHistoryLoggi
    {
        public StatusLoggi Status { get; set; }
        public LocationLoggi Location { get; set; }
    }
}