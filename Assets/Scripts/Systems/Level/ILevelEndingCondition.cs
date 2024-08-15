namespace Game.Systems.Level
{
    public interface ILevelEndingCondition
    {
        public bool IsCompleted { get; }

        public bool[] CompletionList { get; }

        public void Check();
    }
}