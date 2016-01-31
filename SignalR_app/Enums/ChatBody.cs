namespace SignalR_app.Enums
{
    public sealed class ColorConstant
    {
        public static readonly ColorConstant Green = new ColorConstant("green");
        public static readonly ColorConstant Blue = new ColorConstant("blue");
        public static readonly ColorConstant Orange = new ColorConstant("#ff4700");
        public static readonly ColorConstant Red = new ColorConstant("red");
        public static readonly ColorConstant LightGreen = new ColorConstant("lightgreen");
        public static readonly ColorConstant Lightblue = new ColorConstant("lightblue");
        public static readonly ColorConstant Black = new ColorConstant("#000");
        public static readonly ColorConstant Black2 = new ColorConstant("#222");
        public static readonly ColorConstant Black6 = new ColorConstant("#666");
        public static readonly ColorConstant Black9 = new ColorConstant("#999");

        private ColorConstant(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}