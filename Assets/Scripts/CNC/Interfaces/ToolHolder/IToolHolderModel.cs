using System;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.ToolHolder
{
    public interface IToolHolderModel<TTool> 
        where TTool : ITool
    {
        /// <summary>
        /// Ссылка на текущий загруженный инструмент (объект ITool)
        /// Если инструмента нет — null
        /// </summary>
        TTool CurrentTool { get; }
    
        /// <summary>
        /// Номер слота в магазине (1-20), откуда взят инструмент
        /// Если инструмента нет — -1
        /// </summary>
        int CurrentLocation { get; }
    
        /// <summary>
        /// Уникальный идентификатор инструмента в репозитории
        /// Если инструмента нет — -1
        /// </summary>
        int CurrentToolId { get; }
    
        /// <summary>
        /// true — инструмент загружен и готов к работе
        /// false — шпиндель/резцедержатель пуст
        /// </summary>
        bool HasTool { get; }
    
        /// <summary>
        /// Вызывается при успешной загрузке нового инструмента
        /// Передаёт ссылку на загруженный инструмент
        /// </summary>
        event Action<TTool> OnToolChanged;
    
        /// <summary>
        /// Вызывается при выгрузке инструмента (шпиндель опустел)
        /// </summary>
        event Action OnToolUnloaded;

        /// <summary>
        /// Загрузить инструмент из указанного слота магазина
        /// Возвращает true если слот не пуст и загрузка успешна
        /// </summary>
        bool TryLoadTool(TTool tool, int location);
    
        /// <summary>
        /// Выгрузить текущий инструмент (вернуть в магазин или сбросить)
        /// </summary>
        void UnloadTool();
    
        /// <summary>
        /// Попытаться получить текущий инструмент
        /// out T tool — ссылка на инструмент или null
        /// Возвращает true если инструмент есть
        /// </summary>
        bool TryGetTool(out TTool tool);

        /// <summary>
        /// Создаем пустой инструмент вне репозитория Id = -1
        /// </summary>
        TTool GetEmptyTool();
    }
}