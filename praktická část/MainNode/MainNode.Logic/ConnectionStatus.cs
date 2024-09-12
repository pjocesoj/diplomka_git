using MainNode.Logic.Enums;

namespace MainNode.Logic
{
    public class ConnectionStatus
    {
        public static uint MaxFails = 5;
        public static uint recoveryThreshold = 5;

        public ConnectionStatusEnum Status
        {
            get
            {
                if (Fails.Count > 0)
                {
                    if (FailsInRow >= MaxFails) { return ConnectionStatusEnum.LOST; }
                    if (FailsInRow > 0) { return ConnectionStatusEnum.WITH_PROBLEMS; }
                    if (SuccessesInRow >= recoveryThreshold) { return ConnectionStatusEnum.GOOD; }
                    return ConnectionStatusEnum.RECOVERING;
                }
                if (SuccessesInRow > 0) { return ConnectionStatusEnum.GOOD; }

                return ConnectionStatusEnum.UNKNOWN;
            }
        }
        /// <summary>
        /// vsechny chyby co nastaly
        /// </summary>
        public List<ErrorData> Fails { get; private set; } = new List<ErrorData>();

        /// <summary>
        /// celkový počet úspěšných pokusů <br/>
        /// použito jen pro statistiku
        /// </summary>
        public ulong Successes { get; private set; } = 0;
        
        /// <summary>
        /// nepřerušená řada úspěšných pokusů <br/>
        /// použito pro určení statusu
        /// </summary>
        public ulong SuccessesInRow { get; private set; } = 0;
        /// <summary>
        /// nepřerušená řada neúspěšných pokusů <br/>
        /// použito pro určení statusu
        /// </summary>
        public ulong FailsInRow { get; private set; } = 0;


        public void Success()
        {
            Successes++;

            SuccessesInRow++;
            FailsInRow = 0;
        }
        public void Failure(string error)
        {
            Fails.Add(new ErrorData() { Message = error, Time = DateTime.Now });

            SuccessesInRow = 0;
            FailsInRow++;
        }   
        public void Reset()
        {
            Fails.Clear();
            Successes = 0;

            SuccessesInRow = 0;
            FailsInRow = 0;
        }
    }
}
