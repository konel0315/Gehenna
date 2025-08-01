namespace Gehenna
{
    public class GameFlowParam : ManagerParam
    {
        public ResourceManager Resource { get; }
        public AudioManager Audio { get; }
        public InputManager Input { get; }
        public UIManager UI { get; }
        
        public GameFlowParam
        (
            GameConfig gameConfig,
            ResourceManager resource,
            AudioManager audio,
            InputManager input,
            UIManager ui
        ) : base(gameConfig)
        {
            Resource = resource;
            Audio = audio;
            Input = input;
            UI = ui;
        }
    }
}