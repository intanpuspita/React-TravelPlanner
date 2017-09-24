import Constant from '../constants';
import { combineReducers } from 'redux';

export const alertLogin = (state = null, action) => {
    switch(action.type)
    {
        case Constant.SHOW_ALERT_LOGIN :
            return action.payload;

        default :
            return state;
    }
}

export default combineReducers({
    alertLogin
});