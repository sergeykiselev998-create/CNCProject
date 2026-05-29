using System;
using CNC.Implementation.ToolPanel.Views;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;

namespace CNC.Implementation.ToolPanel.Presenters
{
    public abstract class MainBasePresenter<TModel, TTool> : IDisposable 
        where TModel : IToolListModel<TTool> 
        where TTool : IMainData
    {
        private readonly TModel _model;
        private readonly MainToolPanelView _view;

        protected MainBasePresenter(TModel model, MainToolPanelView view)
        {
            _model = model;
            _view = view;
            AddListeners();
        }
        
        private void AddListeners()
        {
            _view.OnNameChanged += UpdateToolNameInModel;
            _view.OnNameChangeFailed += ResetToolNameInControl;
            SubscribeToModel();
        }
        
        private void RemoveListeners()
        {
            _view.OnNameChanged -= UpdateToolNameInModel;
            _view.OnNameChangeFailed -= ResetToolNameInControl;
            UnsubscribeFromModel();
        }

        private void ResetToolNameInControl(int id)
        {
            if (_model.TryGetTool(id, out var tool))
            {
                _view.UpdateToolNameInControl(id, tool.ToolName);
            }
        }
        
        private void UpdateToolNameInControl(int id, string formatName)
        {
            _view.UpdateToolNameInControl(id, formatName);
        }

        private void SubscribeToModel()
        {
            _model.OnToolNameChanged += UpdateToolNameInControl;
        }

        private void UnsubscribeFromModel()
        {
            _model.OnToolNameChanged -= UpdateToolNameInControl;
        }

        private void UpdateToolNameInModel(int id, string formatName)
        {
            _model.UpdateToolName(id, formatName);
        }

        private void AddEdgeInModel(int id)
        {
            
        }

        public virtual void Dispose()
        {
            RemoveListeners();
        }
    }
}