namespace jaslab1
{
    public class House
    {
        public string address { get; set; }
        public int houseroom { get; set; }
        public int floors { get; set; }

        public House(string address, int houseroom, int floors)
        {
            this.address = address;
            this.houseroom = houseroom;
            this.floors = floors;
        }
    }
}