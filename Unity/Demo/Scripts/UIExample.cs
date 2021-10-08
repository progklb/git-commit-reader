using UnityEngine;
using UnityEngine.UI;

namespace GitSnaphot.Demo
{
    /// <summary>
    /// An example script that shows the snapshot in use.
    /// </summary>
    public class UIExample : MonoBehaviour
    {
        #region VARIABLES
        [SerializeField] private Text m_Text;
        #endregion


        #region UNITY EVENTS
        void OnEnable()
        {
            var snapshot = GitInterface.GetSnapshot();

            // Not performant, but this is just an example, right?
            m_Text.text =
                Bold("Commit Hash Long:\n") + snapshot.commitHashLong + Separator() +
                Bold("Commit Hash Short:\n") + snapshot.commitHashShort + Separator() +
                Bold("Relative Tag:\n") + snapshot.relativeTag;
        }
        #endregion


        #region HELPER FUNCTIONS
        string Bold(string text)
            => $"<b>{text}</b>";

        string Separator()
            => $"\n\n";
        #endregion
    }
}
