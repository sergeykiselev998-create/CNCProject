using System;
using CNC.Implementation.ToolPanel.Models;
using CNC.Implementation.ToolPanel.Views;

namespace CNC.Implementation.ToolPanel.Presenters
{
    public class OffsetMillingToolPresenter : IDisposable
    {
        private readonly MillingToolListModel Model;
        private readonly MillingOffsetPanelView View;

        public OffsetMillingToolPresenter(MillingToolListModel model, MillingOffsetPanelView view)
        {
            Model = model;
            View = view;
            
            AddListeners();
        }

        private void AddListeners()
        {
            //Length
            View.OnLengthChanged += UpdateLengthInModel;
            View.OnLengthChangeFailed += ResetLengthInControl;

            Model.OnLengthChanged += UpdateLengthInControl;

            //Diameter
            View.OnDiameterChanged += UpdateDiameterInModel;
            View.OnDiameterChangeFailed += ResetDiameterInControl;
            
            Model.OnDiameterChanged += UpdateDiameterInControl;
        }
        
        private void RemoveListeners()
        {
            //Length
            View.OnLengthChanged -= UpdateLengthInModel;
            View.OnLengthChangeFailed -= ResetLengthInControl;
            
            Model.OnLengthChanged -= UpdateLengthInControl;
            
            //Diameter
            View.OnDiameterChanged -= UpdateDiameterInModel;
            View.OnDiameterChangeFailed -= ResetDiameterInControl;
            
            Model.OnDiameterChanged -= UpdateDiameterInControl;
        }

        //Length
        private void UpdateLengthInModel(int id, int edge, float length)
        {
            Model.UpdateLength(id,edge,length);
        }
        
        private void UpdateLengthInControl(int id, int edge, float length)
        {
            View.UpdateLengthInControl(id, edge, length);
        }
        
        private void ResetLengthInControl(int id, int edge)
        {
            if (!Model.TryGetTool(id, out var tool))
                return;
            
            if (!Model.TryGetOffset(tool, edge, out var offset))
                return;
            
            View.UpdateLengthInControl(id, edge, offset.Length);
        }
        
        //Diameter
        private void UpdateDiameterInModel(int id, int edge, float diameter)
        {
            Model.UpdateDiameter(id, edge, diameter);
        }
        
        private void UpdateDiameterInControl(int id, int edge, float diameter)
        {
            View.UpdateDiameterInControl(id,edge,diameter);
        }
        
        private void ResetDiameterInControl(int id, int edge)
        {
            if (!Model.TryGetTool(id, out var tool))
                return;
          
            if (!Model.TryGetOffset(tool, edge, out var offset))
                return;
            
            View.UpdateDiameterInControl(id, edge, offset.Diameter);
        }

        public void Dispose()
        {
            RemoveListeners();
        }
    }
}