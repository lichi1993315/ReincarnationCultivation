using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using UnityEngine.EventSystems;
using ReincarnationCultivation.Type;

namespace ReincarnationCultivation
{
    public class ItemListUI : MonoBehaviour
    {
        public TextMeshProUGUI descriptionText;
        public RectTransform itemContent;
        public ItemUI itemUIPrefab;
        public Button confirmButton;
        public Button submitButton;
        public Button cancelButton;
        System.Action OnConfirm;
        System.Action<ItemConfig[]> OnSubmit;
        System.Action OnCancel;
        void Start()
        {
            confirmButton.onClick.AddListener(Confirm);
            submitButton.onClick.AddListener(Submit);
            cancelButton.onClick.AddListener(Cancel);
        }
        void Confirm()
        {
            Hide();
            OnConfirm?.Invoke();
        }
        void Submit()
        {
            Hide();
            OnSubmit?.Invoke( 
                itemContent.GetComponentsInChildren<ItemUI>()
                    .Where(e=>e.toggle.isOn)
                    .Select(e=>e.config)
                    .ToArray() );
        }
        void Cancel()
        {
            Hide();
            OnCancel?.Invoke();
        }
        public void Hide()
        {
            Clear();
            gameObject.SetActive(false);
        }
        public void Show(ItemConfig[] items,System.Action onConfirm)
        {
            gameObject.SetActive(true);
            confirmButton.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            OnConfirm = onConfirm;
            _Show(items,false);
        }
        public void Select(ItemConfig[] items,System.Action<ItemConfig[]> onSubmit,System.Action onCancel)
        {
            gameObject.SetActive(true);
            confirmButton.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
            OnSubmit = onSubmit;
            OnCancel = onCancel;
            _Show(items,true);
        }
        void _Show(ItemConfig[] items,bool interactable)
        {
            descriptionText.text = "";
            foreach(var item in items)
                Add(item,interactable);
        }
        void Add(ItemConfig config,bool interactable)
        {
            var item = Instantiate(itemUIPrefab);
            item.transform.SetParent(itemContent,false);
            item.toggle.interactable = interactable;
            item.config = config;
            item.OnPointerOver = ()=>descriptionText.text=string.Join(" ",config.description);
        }
        void Clear()
        {
            for(var i=0;i<itemContent.childCount;++i)
            {
                Destroy(itemContent.GetChild(i).gameObject);
            }
        }
    }
}