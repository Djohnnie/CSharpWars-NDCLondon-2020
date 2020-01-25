using Assets.Scripts.Helpers;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class BotController : MonoBehaviour
    {
        private Bot _bot;
        private Animation _animation;
        private ArenaController _arenaController;
        private NameTagController _nameTagController;
        private StaminaTagController _staminaTagController;

        private string _lastAnimation;

        private GameObject _errorGameObject;

        [Header("The animation speed variables")]
        public float WalkingSpeed = 1;
        public float RotationSpeed = 2;

        [Header("A link to the HEAD of the bot model")]
        public Transform Head;

        [Header("The PREFAB to use for the error exclamation mark")]
        public GameObject ErrorPrefab;

        void Start()
        {
            _animation = gameObject.GetComponentInChildren<Animation>();
            if (_animation != null)
            {
                _animation[Animations.Walk].speed = WalkingSpeed * 2;
                _animation[Animations.Turn].speed = WalkingSpeed * 2;
            }
        }

        void Update()
        {
            if (_bot == null)
            {
                return;
            }

            if (_bot.Move == PossibleMoves.ScriptError)
            {
                RunAnimationOnce(Animations.Defend);

                if (_errorGameObject == null)
                {
                    _errorGameObject = Instantiate(ErrorPrefab);
                    _errorGameObject.transform.SetParent(Head);
                    _errorGameObject.transform.localPosition = new Vector3(0, 0, 0);
                    _errorGameObject.transform.position = new Vector3(_errorGameObject.transform.position.x, 2.5f, _errorGameObject.transform.position.z);
                }

                return;
            }

            if (_errorGameObject != null)
            {
                Destroy(_errorGameObject);
                _errorGameObject = null;
            }

            if (_bot.Move == PossibleMoves.WalkForward)
            {
                var step = WalkingSpeed * Time.deltaTime;
                var targetWorldPosition = _arenaController.ArenaToWorldPosition(_bot.X, _bot.Y);
                var newPos = Vector3.MoveTowards(transform.position, targetWorldPosition, step);
                
                if ((newPos - transform.position).magnitude > 0.01f)
                {
                    RunAnimation(Animations.Walk);
                    transform.position = newPos;
                    return;
                }
            }

            if (_bot.Move == PossibleMoves.TurningLeft || _bot.Move == PossibleMoves.TurningRight || _bot.Move == PossibleMoves.TurningAround)
            {
                var targetOrientation = OrientationVector.CreateFrom(_bot.Orientation);
                var rotationStep = RotationSpeed * Time.deltaTime;
                var newDir = Vector3.RotateTowards(transform.forward, targetOrientation, rotationStep, 0.0F);
                
                if (targetOrientation != newDir)
                {
                    transform.rotation = Quaternion.LookRotation(newDir);
                    RunAnimation(Animations.Turn);
                    return;
                }
            }

            RunAnimation(Animations.Idle);
        }

        void RunAnimation(string animationName)
        {
            if (!_animation.IsPlaying(animationName))
            {
                _animation.Stop();
                _animation.Play(animationName);
                _lastAnimation = null;
            }
        }

        void RunAnimationOnce(string animationName)
        {
            if (!_animation.IsPlaying(animationName) && _lastAnimation != animationName)
            {
                _animation.Stop();
                _animation.Play(animationName);
                _lastAnimation = animationName;
            }

            if (!_animation.IsPlaying(animationName))
            {
                _lastAnimation = null;
            }
        }

        public void UpdateBot(Bot bot)
        {
            _bot = bot;

            if (_staminaTagController != null)
            {
                _staminaTagController.UpdateTag(bot);
            }
        }

        public void SetTagController(TagController tagController)
        {
            switch (tagController)
            {
                case StaminaTagController t:
                    _staminaTagController = t;
                    break;
                case NameTagController t:
                    _nameTagController = t;
                    break;
            }
        }

        public void SetArenaController(ArenaController arenaController)
        {
            _arenaController = arenaController;
        }

        public void InstantRefresh()
        {
            if (_bot == null) return;

            transform.position = _arenaController.ArenaToWorldPosition(_bot.X, _bot.Y);
            transform.eulerAngles = OrientationVector.CreateFrom(_bot.Orientation);
            _lastAnimation = null;
        }

        public void Destroy()
        {
            if (_nameTagController != null)
            {
                _nameTagController.Destroy();
            }

            if (_staminaTagController != null)
            {
                _staminaTagController.Destroy();
            }

            Destroy(gameObject);
            Destroy(this);
        }
    }
}