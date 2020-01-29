# CSharpWars

![CSharp Wars Logo](https://www.djohnnie.be/csharpwars/logo.png "CSharp Wars Logo")

[Return to README](https://github.com/Djohnnie/CSharpWars-NDCLondon-2020)

[Return to step 10](https://github.com/Djohnnie/CSharpWars-NDCLondon-2020/blob/master/workshop/step10/step.md)

## Step 11

Now that are backend and processing middleware is finished, we can finish our frontend.

Go the the frontend scripts solution in Visual Studio 2019 and find the *BotsController.cs* file. Add the following extra code to the *RefreshBots* method:

```c#
private void RefreshBots()
{
    var bots = ApiClient.GetBots();

    foreach (var bot in bots)
    {
        if (!_bots.ContainsKey(bot.Id))
        {
            var newBot = Instantiate(BotPrefab);
            newBot.transform.parent = transform;
            newBot.name = $"Bot-{bot.Name}";
            var botController = newBot.GetComponent<BotController>();
            botController.SetBot(bot);
            botController.SetArenaController(_arenaController);
            botController.InstantRefresh();
            _bots.Add(bot.Id, botController);
        }
        else
        {
            var botController = _bots[bot.Id];
            botController.SetBot(bot);
        }
    }
}
```

This addition will make sure that existing robots will be updated if their state changes.

Next, find the *BotController.cs* file and add the following code to it:

```c#
private Animation _animation;
private string _lastAnimation;

[Header("The animation speed variables")]
public float WalkingSpeed = 2;
public float RotationSpeed = 4;

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
```

Running this in the Unity editor should make your robot move around by turning left indefinitely.