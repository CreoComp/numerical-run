using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;


        // Ваши сохранения

        public int Coins;
        public int Fragments;
        public int ftueLevel;
        public int rank;
        public int usedCharacter;
        public int usedTheme;
        public int usedAccessory;

        public List<string> characters = new List<string>();
        public List<string> themes = new List<string>();

        public Dictionary<Consumable.ConsumableType, int> consumables;
        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        // Пока выявленное ограничение - это расширение массива


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {

            usedCharacter = 0;
            usedTheme = 0;
            usedAccessory = -1;

            Coins = 0;
            Fragments = 0;

            characters.Add("Chef");
            themes.Add("Day");

            ftueLevel = 0;
            rank = 0;

           // CheckMissionsCount();
        }
    }
}
