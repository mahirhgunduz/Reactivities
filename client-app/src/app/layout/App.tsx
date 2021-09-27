import {Fragment, useEffect, useState} from 'react';
import axios  from 'axios';
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import { Activity } from '../models/activity';

import {v4 as uid} from 'uuid'

function App() {

  const [activities,setActivities] = useState<Activity[]>([]);

  const [selectedActivity,setSelectedActivity] = useState<Activity | undefined>(undefined);

  const [editMode,setEditMode] = useState(false);

  useEffect(()=>{
     

      axios.get<Activity[]>("http://localhost:5000/api/activities")
           .then(response => { 
                          setActivities(response.data); 
                             });
  },[])

  const handleSelectedActivity= (id:string)=> {
    setSelectedActivity(activities.find(p => p.id === id));
  }

  const handleCancelSelectActivity = () =>{
    setSelectedActivity(undefined);
  }

  const handleFormOpen = (id?:string) => {

    id ? handleSelectedActivity(id): handleCancelSelectActivity();
    setEditMode(true);
  }

  const handleFormClose = () =>{
    setEditMode(false);
  }

  function handleCreateOrEditActivity(activity:Activity){
    activity.id ? 
    setActivities([...activities.filter(p=>p.id!==activity.id),activity]) :
    setActivities([...activities,{...activity, id:uid() }]);

    setEditMode(false);
    setSelectedActivity(activity);

    console.log(activities);
  }

  function handleDeleteActivity(id:string){

    setActivities([...activities.filter(p=>p.id!==id)]);
  }

  
  return (
    <Fragment>
        <NavBar openForm = {handleFormOpen} />
        <Container style={{marginTop:'7em'}}>
          <ActivityDashboard 
            activities={activities}
            selectedActivity = {selectedActivity}
            selectActivity = {handleSelectedActivity}
            cancelSelectActivity = {handleCancelSelectActivity}
            editMode = {editMode}
            openForm = {handleFormOpen}
            closeForm = {handleFormClose}
            createOrEdit = {handleCreateOrEditActivity}
            deleteActivity = { handleDeleteActivity }
          />
        </Container>
    </Fragment>
  );
}

export default App;
