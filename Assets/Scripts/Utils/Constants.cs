public static class CONSTANTS
{
    public static class GLOBAL
    {
        public const float  TIME_BEFORE_DESTROY = 5.0f;

        public static class SCENES
        {
            public const string MENU = "MENU";
            public const string PRACTICE_STORY = "PRACTICE_STORY";
            public const string DRY_PLANET_INTRO = "DRY_PLANET_INTRO";
            public const string DRY_PLANET = "DRY_PLANET";
            public const string GREEN_PLANET_INTRO = "GREEN_PLANET_INTRO";
            public const string GREEN_PLANET = "GREEN_PLANET";
            public const string LUSH_PLANET_INTRO = "LUSH_PLANET_INTRO";
            public const string LUSH_PLANET = "LUSH_PLANET";
            public const string ICE_PLANET_INTRO = "ICE_PLANET_INTRO";
            public const string ICE_PLANET = "ICE_PLANET";
            public const string GAME_OVER = "GAME_OVER";
            public const string ENDING = "ENDING";
        }
    }
    public static class ENEMY
    {
        public static class BOSS
        {
            // Stats
            public const float  HP = 2000.0f;
            public const float  DAMAGES = 20.0f;
            public const float  SPEED = 0.0f;
            public const float  FIRE_RATE = 1.0f;
            // Distances State Change
            public const float  PATROL_DISTANCE = 10.0f;
            public const float  CHASING_DISTANCE = 7.0f;
            public const float  ATTACK_DISTANCE = 4.0f;
            public const int    REWARD = 150;
            // Animations names
            public const string ANIM_TAKE_DAMAGE = "Take Damage";
            public const string ANIM_IDLE = "Idle";
            public const string ANIM_DIE = "Die";
            public const string ANIM_RUN = "Run Forward";
            public const string ANIM_ATTACK = "Leg Attack";
        }
    }

    public static class GAME_MANAGER
    {
        
    }
}
