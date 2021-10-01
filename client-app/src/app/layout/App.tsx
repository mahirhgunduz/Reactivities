import {Fragment, useEffect, useState} from 'react';

import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import { Activity } from '../models/activity';

import {v4 as uid} from 'uuid'
import agent from '../api/agent';

import LoadingComponent from './LoadingComponent';


function App() {

  const [activities,setActivities] = useState<Activity[]>([]);

  const [selectedActivity,setSelectedActivity] = useState<Activity | undefined>(undefined);

  const [editMode,setEditMode] = useState(false);

  const [isLoading,setIsLoading] = useState(true);

  const [submitting,setSubmitting] = useState(false);

  useEffect(()=>{
     
        setIsLoading(true);

        GetActivities();


      // axios.get<Activity[]>("http://localhost:5000/api/activities")
      //      .then(response => { 
      //                     setActivities(response.data); 
      //                        });
  },[])

  const handleSelectedActivity= (id:string)=> {

    agent.Activities.details(id).then(response=>{
      setSelectedActivity(response);
    })

    
  }

  const GetActivities = () => {

    agent.Activities.list().then(response => { 

      let activities : Activity[] = [];

       response.forEach(activity=>{
       
         activity.date = activity.date.split('T')[0];
         
         activities.push(activity);
         
       });
      
       setActivities(activities);
       setIsLoading(false);
       setSubmitting(false);
    })
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

    setSubmitting(true);

    if(activity.id){
      agent.Activities.update(activity.id,activity).then(response=>{
          GetActivities();
          setSubmitting(false);
          setEditMode(false);
      });
    }else{
      //activity = {...activity, id:uid() };
      activity.id = uid();

      agent.Activities.create(activity).then(response=>{
        GetActivities();
        setSubmitting(false);
        setEditMode(false);
      });
    }


   
    setSelectedActivity(activity);
    

    
  }

  function handleDeleteActivity(id:string){
    setSubmitting(true);
    
    agent.Activities.delete(id).then(response=>{
      GetActivities();
    });
  }



  return (
    <Fragment>
        <NavBar openForm = {handleFormOpen} />
        <Container style={{marginTop:'7em'}}>
        
          {isLoading && <LoadingComponent/>}

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
            submitting= {submitting}
          />
        </Container>
    </Fragment>
  );
}

export default App;
