import ActivityStore from "./activityStores";
import UserStore from "./userStore";
import CommonStore from "./commonStore";
import ModalStore from "./modalStore";
import ProfileStore from "./profileStore";
import {createContext, useContext} from 'react'; 

interface Store {
    activityStore: ActivityStore
    userStore: UserStore
    commonStore: CommonStore
    modalStore: ModalStore
    profileStore:ProfileStore
}

export const store:Store={
     activityStore: new ActivityStore(),
     userStore: new UserStore(),
     commonStore: new CommonStore(),
     modalStore: new ModalStore(),
     profileStore : new ProfileStore()
}


export const StoreContext = createContext(store);

export const useStore = ()=>{
    return useContext(StoreContext);
}


