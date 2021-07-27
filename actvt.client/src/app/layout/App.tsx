import LoadingComponent from './LoadingComponent';
import React,{ useEffect, useState} from 'react';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import {Activity} from '../models/activity';
import NavBar from './NavBar';
import {Container} from 'semantic-ui-react';
import {v4 as uuid} from 'uuid';
import  agent  from '../api/agent';



function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined);
  const [editMode, setEditMode] = useState(false);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [deleting, setDeleting] = useState(false);
  //const [deleteId, setDeleteId] = useState('');

  const handleSelectedActivity = (id:string) =>{
      setSelectedActivity(activities.find(x =>x.id === id))
  }

  const handleCancelSelectActivity = ()=>{
       setSelectedActivity(undefined);
  }

  const handleFormOpen = (id?:string) =>{
       id ? handleSelectedActivity(id) : handleCancelSelectActivity();
       setEditMode(true);
  }

  const handleFormClose = () =>{
    setEditMode(false);
  }

  const handleCreateOrEditActivity = (activity:Activity) =>{
      setSubmitting(true);
      if(activity.id){
        agent.Activities.update(activity).then(()=>{
          setActivities([...activities.filter(x => x.id !== activity.id), activity])
          setSelectedActivity(activity)
          setEditMode(false);
          setSubmitting(false);
        })
      }else{
        activity.id = uuid();
        agent.Activities.create(activity).then(()=>{
          setActivities([...activities, {...activity, id:uuid()}])
          setSelectedActivity(activity)
          setEditMode(false);
          setSubmitting(false);
        })
      }
      
  }


  const handleDeleteActivity = (id:string) =>{
    setSubmitting(true);
    //setDeleteId(id);
    agent.Activities.delete(id).then(()=>{
      setActivities([...activities.filter(x => x.id !== id)])
      setSubmitting(false);
      //setDeleting(false);
      //setDeleteId('');
      
    })
    
  }


  

  useEffect(() =>{
     agent.Activities.list()
           .then((res)=>{
             let activities  : Activity[] = [];
             res.forEach(activity =>{
               activity.date = activity.date.split('T')[0];
               activities.push(activity);
             })
              setActivities(activities);
              setLoading(false);
           })

  },[])
  
  if(loading) return <LoadingComponent content={'App is Loading'}/>


  return (
    <>
        <NavBar openForm={handleFormOpen}/>
        <Container style={{marginTop:'7em'}}>
          <ActivityDashboard 
          activities={activities}
          selectedActivity={selectedActivity}
          selectActivity={handleSelectedActivity}
          cancelSelectActivity={handleCancelSelectActivity}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm= {handleFormClose}
          createOrEdit={handleCreateOrEditActivity}
          deleteActivity={handleDeleteActivity}
          submitting={submitting}
          //deleting={deleting}
          //deleteId ={deleteId}
          />
        </Container>
        
    </>
  );
}

export default App;
