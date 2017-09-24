import Constant from './constants';

export function showAlertLogin(show = false) {
    return {
        type: Constant.SHOW_ALERT_LOGIN,
        payload: show
    }
}