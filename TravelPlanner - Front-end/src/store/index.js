import Constant from '../constants';
import appReducer from './reducers';
import thunk from 'redux-thunk';
import { createStore, applyMiddleware } from 'redux';

const consoleMessages = store => next => action => {
    let result;
    console.log("dispatching action => " + action.type);
    //console.log(action);
    result = next(action);
    //console.log(store.getState());
    return result;
}

export default (initialState={}) => {
    return applyMiddleware(thunk, consoleMessages)(createStore)(appReducer, initialState);
}