using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneSound 
{
    //�^�C�g����ʂ̉��̔ԍ��̗񋓑�
    enum Title 
    {

        system,


    }

    //�Z���N�g��ʂ̉��̔ԍ��̗񋓑�
    enum Select 
    {
        system,


    }

    //�C���Q�[���Ŏg�����̗񋓑�
    enum main 
    {
        
        monster,
        system








    }

    enum Dragon 
    {
        DragonMove,
        DragonMoveVoice,
        DragonVoice,
        DragonAttackHit,
        DragonJumpStart,
        DragonJumpEnd,
        DragonLongAttack,
        DragonBite,
        DragonRoar,
        DragonBigRoar,
        DragonHitVoice,//�U����H�������̐�
        DragonNormalDown,
        DragonNormalDownVoice,
        DragonHardDown,//�_�E�����̕���
        DragonHardDownVoice,//�_�E�����̋��ѐ�
        Max
        //�����܂łǂ炲��

    }
    enum Spider 
    {
        SpiderMove,
        SpiderHitVoice,//�U����H�������̐�
        SpiderAttackHIt,
        SpiderJumpStart,
        SpiderJumpSpace,
        SpiderJumpEnd,
        SpiderCaptorStart,
        SpiderCaptorSuccess,//�ߊl����
        SpiderCaptorMiss,
        SpiderTrapCreation,
        SpiderTrapSnagged,//�g���b�v�Ɉ�������������
        SpiderTrapLoop,//�g���b�v�Ɉ����������Ă��鎞�̉�
        SpiderTrapEnd,//�g���b�v�������Ȃ鉹
        SpiderNormalDown,
        SpiderNormalDownVoice,
        SpiderHardDown,//�_�E�����̕���
        SpiderHardDownVoice,//�_�E�����̋��ѐ�
        //�����܂Œw�

    }

    enum System
    {
        BGM,
        Decision,
    }


}
