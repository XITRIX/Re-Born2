using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VNSelect : Unit, IBranchUnit
{
    // Using L<KVP> instead of Dictionary to allow null key
        [DoNotSerialize]
        public List<KeyValuePair<string, ControlOutput>> Branches { get; private set; }

        [Inspectable, Serialize]
        public List<string> Options { get; set; } = new();

        /// <summary>
        /// The entry point for the switch.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter { get; private set; }

        public override bool canDefine => Options != null;

        protected override void Definition()
        {
            enter = ControlInputCoroutine(nameof(enter), RunCoroutine);

            // selector = ValueInput<string>(nameof(selector));

            // Requirement(selector, enter);

            Branches = new List<KeyValuePair<string, ControlOutput>>();

            foreach (var option in Options)
            {
                var key = "%" + option;

                if (!controlOutputs.Contains(key))
                {
                    var branch = ControlOutput(key);
                    Branches.Add(new KeyValuePair<string, ControlOutput>(option, branch));
                    Succession(enter, branch);
                }
            }

            // @default = ControlOutput(nameof(@default));
            // Succession(enter, @default);
        }

        protected virtual bool Matches(string a, string b)
        {
            return Equals(a, b);
        }
        
        IEnumerator RunCoroutine(Flow flow)
        {
            PlayerInputScript.Shared.DisablePlayerInput();
            
            var dialogOptionHolder = UIDialogMessage.Shared.dialogOptionsHolder.transform;
            foreach (Transform child in dialogOptionHolder) {
                Object.Destroy(child.gameObject);
            }
            
            var selected = "";
            foreach (var branch in Branches)
            {
                var buttonObject = Object.Instantiate(UIDialogMessage.Shared.dialogOptionButtonPrefab, dialogOptionHolder.transform);
                var button = buttonObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    selected = branch.Key;
                });

                buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = branch.Key;
            }
            
            // Make first button active
            if (dialogOptionHolder.childCount > 0)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(dialogOptionHolder.GetChild(0).gameObject);
            }
            
            yield return new WaitUntil(() => !string.IsNullOrEmpty(selected));
            
            foreach (Transform child in dialogOptionHolder) {
                Object.Destroy(child.gameObject);
            }
            
            PlayerInputScript.Shared.EnablePlayerInput();
            yield return Branches.FirstOrDefault(x => x.Key == selected).Value;
        }
}
