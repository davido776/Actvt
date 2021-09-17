import React, { useEffect } from 'react';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import NavBar from './NavBar';
import {Container} from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { Route, Switch, useLocation } from 'react-router-dom';
import Homepage from '../../features/home/Homepage';
import ActivityForm from '../../features/activities/form/ActivityForm';
import ActivityDetails from '../../features/activities/details/ActivityDetails';
import { ToastContainer } from 'react-toastify';
import LoginForm from '../../features/users/LoginForm';
import NotFound from '../../features/errors/NotFound';
import { useStore } from '../stores/store';
import ProfilePage from '../../features/profiles/ProfilePage';
import ModalContainer from '../common/modals/ModalContainer';
import LoadingComponent from './LoadingComponent';



function App() {
  const location = useLocation();
  const {commonStore, userStore}  = useStore();

  useEffect(()=>{
    if(commonStore.token){
      userStore.getUser().finally(()=>commonStore.setAppLoaded());
    }else{
      commonStore.setAppLoaded();
    }
  },[commonStore, userStore])

  if(!commonStore.appLoaded) return <LoadingComponent content='Loading app...' />
  
  return (
    <>
        <ToastContainer position='bottom-right' hideProgressBar/>
        <ModalContainer/>
        <Route exact path='/' component={Homepage} />
        <Route
           path={'/(.+)'}
           render = {()=>(
            <>
              <NavBar/>
              <Container style={{marginTop:'7em'}}>
                <Switch>
                  <Route exact path='/activities' component={ActivityDashboard} />
                  <Route path='/activities/:id' component={ActivityDetails} />
                  <Route key={location.key} path={['/createActivity','/manage/:id']} component={ActivityForm} />
                  <Route path='/profiles/:username' component={ProfilePage} />
                  <Route path='/login' component={LoginForm} />
                  <Route component={NotFound}/>
                </Switch>
              </Container>
            </>
            
            )}
        />
        
    </>
  );
}

export default observer(App);
