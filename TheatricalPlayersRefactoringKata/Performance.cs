namespace TheatricalPlayersRefactoringKata
{
    public class Performance
    {
        private string _playID;
        private int    _audienceSize;

        public string PlayID       { get => _playID      ; set => _playID = value; }
        public int    AudienceSize { get => _audienceSize; set => _audienceSize = value; }

        public Performance(string playID, int audience)
        {
            this._playID = playID;
            this._audienceSize = audience;
        }

    }
}
