
namespace SharpGame.Internal
{
    public interface IGameEntity
    {
        void Awake();

        void Start();

        void Update(float deltaTime);

        void Draw(float deltaTime);

        void OnDestroy();
    }
}