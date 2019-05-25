using Halcyon.HAL;

namespace B2Handpicked.ApplicationServices {
    public static class HalExtensions {
        public static HALResponse AddLink(this HALResponse hal, Link link) => hal.AddLinks(new Link[] { link });

        public static HALResponse AddLinkToSelf(this HALResponse hal, string href) => hal.AddLinks(new Link[] { new Link("self", href) });
    }
}
