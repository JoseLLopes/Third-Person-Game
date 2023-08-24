using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Pagination{
    public class PaginationSystem : MonoBehaviour
    {
        [SerializeField] List<Page> allPages;
        public void ShowLonely(Page page){
            foreach(Page pageObj in allPages){
                    pageObj.gameObject.SetActive(false);
            }
            page.gameObject.SetActive(true);
        }
    }
}