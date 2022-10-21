using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinCat.Context;
using WinCat.Model;

namespace WinCat.ViewModel
{
    public class CodeRunnerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Script> m_scriptsList;
        private DataContext m_dataContext = new DataContext();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
        // New script part
        private string m_newName;
        public string NewName { 
            get
            {
                return m_newName;
            }
            set
            {
                m_newName = value;
                NotifyPropertyChanged(nameof(NewName));
            }
        }

        private string m_newPath;
        public string NewPath
        {
            get
            {
                return m_newPath;
            }
            set
            {
                m_newPath = value;
                NotifyPropertyChanged(nameof(NewPath));

            }
        }

        private string m_NewDescription;
        public string NewDescription
        {
            get
            {
                return m_NewDescription;
            }
            set
            {
                m_NewDescription = value;
                NotifyPropertyChanged(nameof(NewDescription));
            }
        }

        public CodeRunnerViewModel()
        {
            m_scriptsList = new ObservableCollection<Script>(m_dataContext.Scripts.ToList());
        }

        public ObservableCollection<Script> ScriptsList
        {
            get { return m_scriptsList; }
        }

        public void refresh()
        {

        }

        public async void AddScript()
        {
            Script newScript = new Script
            {
                Name = NewName,
                Path = NewPath,
                Description = NewDescription
            };
            NewName = string.Empty;
            NewPath = string.Empty;
            NewDescription = string.Empty;

            m_scriptsList.Add(newScript);
            NotifyPropertyChanged(nameof(ScriptsList));
            m_dataContext.Scripts.Add(newScript);
            await m_dataContext.SaveChangesAsync();
        }

        public bool RemoveScript(Script oldSCript)
        {
            m_dataContext.Remove(oldSCript);
            m_dataContext.SaveChanges();
            bool result =  m_scriptsList.Remove(oldSCript);
            NotifyPropertyChanged(nameof(ScriptsList));
            return result;
        }

    }
}
