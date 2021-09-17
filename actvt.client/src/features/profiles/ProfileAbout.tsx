import React, { useState } from 'react'
import { Button, Form, Grid, Header, Tab } from 'semantic-ui-react';
import ProfilePhotos from './ProfilePhotos';
import { Profile } from '../../app/models/profile';
import { observer } from 'mobx-react-lite';
import { Formik } from 'formik';
import MyTextArea from "../../app/common/form/MyTextArea";
import MyTextInput from "../../app/common/form/MyTextInput";
import { useStore } from "../../app/stores/store";
import * as Yup from 'yup';


interface Props {
 setEditMode: (editMode: boolean) => void;
}

export default observer(function ProfileAbout(){
    const [editProfileMode, setEditProfileMode] = useState(false);

    const {profileStore: {isCurrentUser,profile, updateProfile}} = useStore()
    const [editMode, setEditMode] = useState(false)

    return(
        <Tab.Pane>
            <Grid>
                <Grid.Column width={16}>
                    <Header floated='left' icon='user' content={`About ${profile?.displayName}`}/>
                    {isCurrentUser &&(
                        <Button
                        floated='right' 
                        basic
                        content={editProfileMode ? 'Cancel': 'Edit Profile'}
                        onClick={() => setEditProfileMode(!editProfileMode)}
                      />
                    )}
                    
                </Grid.Column>
                <Grid.Column width={16}>
                    {editProfileMode ? (
                            <Formik
                            initialValues={{displayName: profile?.displayName, bio:
                            profile?.bio}}
                                onSubmit={(values) => 
                                    updateProfile(values).then(() => {
                                        setEditMode(false);
                                        })
                                }
                                validationSchema={Yup.object({
                                displayName: Yup.string().required()
                                })}
                            >
                            {({handleSubmit,isSubmitting, isValid, dirty}) => (
                            <Form className='ui form' onSubmit={handleSubmit}>
                                <MyTextInput placeholder='Display Name'
                                name='displayName' />
                                <MyTextArea rows={3} placeholder='Add your bio'
                                name='bio' />
                                <Button 
                                    positive
                                    type='submit'
                                    loading={isSubmitting}
                                    content='Update profile'
                                    floated='right'
                                    disabled={!isValid || !dirty}
                                />
                            </Form>
                            )}
                            </Formik>
                    ):(
                        <span style={{whiteSpace: 'pre-wrap'}}>{profile?.bio}</span>
                    )}

                </Grid.Column>
            </Grid>
        </Tab.Pane>
    )
})