namespace Application.Core
{
    public sealed class MotorStatus
    {
        public decimal Amps { get; set; }

        public double Frequency { get; set; }

        public decimal RPM { get; set; }

        public int Speed { get; set; }

        public decimal Volts { get; set; }

        public decimal Watts { get; set; }

        public bool Running { get; set; }
    }
}
