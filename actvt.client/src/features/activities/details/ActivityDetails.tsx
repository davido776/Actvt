import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Grid} from 'semantic-ui-react';
import LoadingComponent from '../../../app/layout/LoadingComponent';
import { useStore } from '../../../app/stores/store';
import ActivityDetailHeader from './ActivityDetailHeader';
import ActivityDetailInfo from './ActivityDetailInfo';
import ActivityDetailSidebar from './ActivityDetailSidebar'; 
import ActivityDetailChat from './ActivityDetailChat';
import { clear } from 'console';


export default observer(function ActivtityDetails() {
    const {activityStore} = useStore();
    const {selectedActivity :activity,loadActivity,loadingInitial,clearSelectedActivity} = activityStore;
    const {id} = useParams<{id:string}>();

    useEffect(()=>{
        if(id) loadActivity(id);
        return () => clearSelectedActivity();
    },[id,loadActivity,clearSelectedActivity]);

    if(loadingInitial || !activity) return <LoadingComponent/>;

    return (
        <Grid>
            <Grid.Column width={10}>
                <ActivityDetailHeader activity={activity}/>
                <ActivityDetailInfo activity={activity}/>
                <ActivityDetailChat activityId={activity.id} />    
            </Grid.Column>
            <Grid.Column width={6}>
                <ActivityDetailSidebar activity={activity}/>
            </Grid.Column>
        </Grid>
    )
})