//Định nghĩa router đến các component

import FixedAssetPage from './views/Asset/FixedAssetPage.vue'
import UsedFixedAssetPage from './views/UsedFixedAsset/UsedFixedAssetPage.vue'
export const routes = [
    {path : '/fixedasset' , name : "Tài sản" , component : FixedAssetPage},
    {path : '/usedFixedAsset' , name : "Ghi tăng" , component : UsedFixedAssetPage}
]

