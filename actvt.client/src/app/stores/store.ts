import ActivityStore from "./activityStores";
import UserStore from "./userStore";
import CommonStore from "./commonStore";
import ModalStore from "./modalStore";
import ProfileStore from "./profileStore";
import CommentStore from "./commentStore";
import {createContext, useContext} from 'react'; 

interface Store {
    activityStore: ActivityStore
    userStore: UserStore
    commonStore: CommonStore
    modalStore: ModalStore
    profileStore:ProfileStore
    commentStore: CommentStore
}

export const store:Store={
     activityStore: new ActivityStore(),
     userStore: new UserStore(),
     commonStore: new CommonStore(),
     modalStore: new ModalStore(),
     profileStore : new ProfileStore(),
     commentStore : new CommentStore()
}


export const StoreContext = createContext(store);

export const useStore = ()=>{
    return useContext(StoreContext);
}


